//-----------------------------------------------------------------------------
//----- Utility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "assert.h"
#include "math.h"
#include "adslib.h"

#include "gepnt3d.h"
#include "gearc2d.h"
#include "gearc3d.h"

#include "dbspline.h"
#include "dbents.h"
#include "dbsymtb.h"

#include "dbapserv.h"
#include "tchar.h"

#include "Utility.h"

AcDbObjectId Utility::CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale)
{
	AcDbObjectId id;

	AcDbTextStyleTable *pStyleTbl = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	pDb->getSymbolTable(pStyleTbl, AcDb::kForRead);
	if (pStyleTbl->getAt(name, id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		pStyleTbl->upgradeOpen();
		AcDbTextStyleTableRecord* pText = new AcDbTextStyleTableRecord();
		pText->setName(name);
		pText->setFileName(filename);
		pText->setXScale(scale);
		pStyleTbl->add(id, pText);
		pText->close();
		pStyleTbl->downgradeOpen();
	}
	pStyleTbl->close();

	return id;
}

Acad::ErrorStatus Utility::MakeGiTextStyle(AcGiTextStyle &newStyle, const AcDbObjectId styleId)
{
    AcDbTextStyleTableRecord *oldStyle;
    Acad::ErrorStatus es = acdbOpenAcDbObject((AcDbObject *&)oldStyle, styleId, AcDb::kForRead);
    if (es == Acad::eOk) 
	{
        const ACHAR *tmpStr;
        if ((es = oldStyle->fileName(tmpStr)) != Acad::eOk) 
		{
            oldStyle->close();
            return es;
        }
        newStyle.setFileName(tmpStr);

        if ((es = oldStyle->bigFontFileName(tmpStr)) != Acad::eOk) 
		{
            oldStyle->close();
            return es;
        }
        newStyle.setBigFontFileName(tmpStr);

        newStyle.setTextSize(oldStyle->textSize());
        newStyle.setXScale(oldStyle->xScale());
        newStyle.setObliquingAngle(oldStyle->obliquingAngle());

        oldStyle->close();
        newStyle.loadStyleRec();
    }
    return es;
}

const AcDbObjectId Utility::GetZeroLayer(void)
{
	AcDbObjectId entId = AcDbObjectId::kNull;
	AcDbLayerTable* pLayerTable = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	if(pDb->getLayerTable(pLayerTable, AcDb::kForRead) == Acad::eOk)
	{
		if(pLayerTable->getAt(_T("0"), entId, AcDb::kForRead) != Acad::eOk)
		{
			entId = AcDbObjectId::kNull;
		}
		pLayerTable->close();
	}
	return entId;
}

const AcDbObjectId Utility::GetDefpointsLayer(void)
{
	AcDbObjectId entId = AcDbObjectId::kNull;
	AcDbLayerTable* pLayerTable = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	if(pDb->getLayerTable(pLayerTable, AcDb::kForRead) == Acad::eOk)
	{
		// Create the layer if it does not exist
		if(!pLayerTable->has(_T("Defpoints")))
		{
			if(pLayerTable->upgradeOpen() == Acad::eOk)
			{
				AcDbLayerTableRecord* pLayer = new AcDbLayerTableRecord;
				pLayer->setName(_T("Defpoints"));
				pLayer->setIsFrozen(0);
				pLayer->setIsOff(0);
				pLayer->setIsLocked(0);
				pLayerTable->add(pLayer);
				pLayer->close();
				pLayerTable->downgradeOpen();
			}
		}

		if(pLayerTable->getAt(_T("Defpoints"), entId, AcDb::kForRead) != Acad::eOk)
		{
			entId = AcDbObjectId::kNull;
		}
		pLayerTable->close();
	}
	return entId;
}