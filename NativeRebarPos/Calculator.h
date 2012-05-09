//-----------------------------------------------------------------------------
//----- Calculator.h : Declaration of the CCalculator
//-----------------------------------------------------------------------------
#pragma once

#include <iostream>
#include <string>
#include <stack>
#include <vector>
#include <sstream>

class CCalculator 
{
private:
	static int OpPrecendence(wchar_t op);
	static bool IsOp(wchar_t op);
	static bool IsLeftParen(wchar_t op);
	static bool IsRightParen(wchar_t op);
	static bool IsNumericChar(wchar_t op);

public:
	static double CalcInfix(std::wstring str);
	static double CalcRPN(std::wstring str);
	static std::wstring InfixToRPN(std::wstring str);
};
