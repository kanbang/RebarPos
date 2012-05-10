//-----------------------------------------------------------------------------
//----- Utility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <msclr\marshal_cppstd.h>
#include "..\NativeRebarPos\Calculator.h"
#include "MgUtility.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Functions
//*************************************************************************
double Utility::EvaluateFormula(System::String^ formula)
{
	try
	{
		return Calculator::Evaluate(msclr::interop::marshal_as<std::wstring>(formula));
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

bool Utility::ValidateFormula(System::String^ formula)
{
	return Calculator::IsValid(msclr::interop::marshal_as<std::wstring>(formula));
}
