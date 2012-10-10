//-----------------------------------------------------------------------------
//----- Utility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include <sstream>
#include <iomanip>

#include "Utility.h"

AcDbObjectId Utility::CreateHiddenLayer(const ACHAR* name, const AcCmColor& color)
{
    AcDbObjectId id;

	AcDbLayerTable *pLayerTbl = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	pDb->getSymbolTable(pLayerTbl, AcDb::kForRead);
	if (pLayerTbl->getAt(name, id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		pLayerTbl->upgradeOpen();
		AcDbLayerTableRecord* pLayer = new AcDbLayerTableRecord();
		pLayer->setName(name);
		pLayer->setColor(color);
		pLayer->setIsPlottable(false);
		pLayerTbl->add(id, pLayer);
		pLayer->close();
		pLayerTbl->downgradeOpen();
	}
	pLayerTbl->close();

	return id;
}

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

const void Utility::ReplaceString(std::wstring& str, const std::wstring& oldStr, const std::wstring& newStr)
{
	size_t pos = 0;
	while((pos = str.find(oldStr, pos)) != std::string::npos)
	{
		str.replace(pos, oldStr.length(), newStr);
		pos += newStr.length();
	}
}

const void Utility::IntToStr(const int val, std::wstring& str)
{
	std::wstringstream s;
	s << val;
	str = s.str();
}

const int Utility::DoubleToInt(const double val)
{
	 return (int)(val >= 0 ? val + 0.5 : val - 0.5);
}

const int Utility::StrToInt(const std::wstring& str)
{
	return Utility::StrToInt(str.c_str());
}

const int Utility::StrToInt(const wchar_t* str)
{
	return _wtoi(str);
}

const void Utility::DoubleToStr(const double val, const int digits, std::wstring& str)
{
	std::wstringstream s;
	s << std::fixed << std::setprecision(digits) << val;
	str = s.str();
}

const void Utility::DoubleToStr(const double val, std::wstring& str)
{
	std::wstringstream s;
	s << val;
	str = s.str();
}

const double Utility::StrToDouble(const std::wstring& str)
{
	return Utility::StrToDouble(str.c_str());
}

const double Utility::StrToDouble(const wchar_t* str)
{
	return _wtof(str);
}

const Acad::ErrorStatus Utility::ReadDXFItem(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, resbuf* rb)
{
	Acad::ErrorStatus es;
	if ((es = pFiler->readItem(rb)) != Acad::eOk)
	{
		pFiler->pushBackItem();
		pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: unable to read group code %d for %s"), code, name);
		return pFiler->filerStatus();
	}
	else if(rb->restype != code)
	{
		pFiler->pushBackItem();
		pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: expected group code %d for %s, but got %d"), code, name, rb->restype);
		return Acad::eMissingDxfField;
	}

	return pFiler->filerStatus();
}

const Acad::ErrorStatus Utility::ReadDXFInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, short& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFUInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned short& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFReal(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, double& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rreal;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFString(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, ACHAR*& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		acutDelString(val);
		val = NULL;
		if(rb.resval.rstring != NULL)
		{
			acutUpdString(rb.resval.rstring, val);
		}
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint2d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint3d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
		val.z = rb.resval.rpoint[2];
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector2d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector3d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
		val.z = rb.resval.rpoint[2];
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, Adesk::Boolean& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, bool& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = (rb.resval.rint != 0);
	}
	return es;
}

const Acad::ErrorStatus Utility::ReadDXFObjectId(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcDbObjectId& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		if((es = acdbGetObjectId(val, rb.resval.rlname)) != Acad::eOk)
		{
			val = AcDbObjectId::kNull;
		}
	}
	return es;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s, wchar_t delim)
{
	std::vector<std::wstring> elems;

	std::wstringstream ss(s);
	std::wstring item;
	while(std::getline(ss, item, delim)) 
	{
		elems.push_back(item);
	}

	return elems;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s)
{
	return SplitString(s, L'\n');
}
