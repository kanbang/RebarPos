//-----------------------------------------------------------------------------
//----- GenericTable.cpp : Implementation of CGenericTable
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "GenericTable.h"

Adesk::UInt32 CGenericTable::kCurrentVersionNumber = 1;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CGenericTable, AcDbEntity,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, GENERICTABLE,
	"OZOZRebarPos\
	|Product Desc:     Geberic Table Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CGenericTable::CGenericTable(void) : m_Rows(0), m_Columns(0), m_Cells(0)
{
}

CGenericTable::~CGenericTable(void)
{
}
