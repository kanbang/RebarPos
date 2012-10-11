//-----------------------------------------------------------------------------
//----- PosUtility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "..\NativeRebarPos\Calculator.h"
#include "..\NativeRebarPos\Utility.h"
#include "MgUtility.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Functions
//*************************************************************************
double PosUtility::EvaluateFormula(System::String^ formula)
{
	try
	{
		return Calculator::Evaluate(Marshal::StringToWstring(formula));
	}
	catch(Calculator::Errors err)
	{
		switch(err)
		{
		case Calculator::eParenthesisMisMatch:
			throw gcnew System::Exception("Paranthesis do not match.");
			break;
		case Calculator::eNotAnOperator:
			throw gcnew System::Exception("Not an operator.");
			break;
		case Calculator::eUnknownOperator:
			throw gcnew System::Exception("Unknown operator.");
			break;
		case Calculator::eInvalidFormula:
			throw gcnew System::Exception("Invalid formula.");
			break;
		default:
			throw gcnew System::Exception("Calculator error.");
			break;
		}
	}
	catch(...)
	{
		throw gcnew System::Exception("Unable to evaluate formula.");
	}
}

bool PosUtility::ValidateFormula(System::String^ formula)
{
	return Calculator::IsValid(Marshal::StringToWstring(formula));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosUtility::CreateTextStyle(System::String^ name, System::String^ filename, double scale)
{
	return Marshal::ToObjectId(Utility::CreateTextStyle(Marshal::StringToWchar(name), Marshal::StringToWchar(filename), scale));
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosUtility::DefpointsLayer::get()
{
	return Marshal::ToObjectId(Utility::CreateHiddenLayer());
}
