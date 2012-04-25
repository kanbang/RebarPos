//-----------------------------------------------------------------------------
//----- Calculator.h : Declaration of the CCalculator
//-----------------------------------------------------------------------------
#pragma once

#include <iostream>
#include <string>
#include <stack>
#include <sstream>

class CCalculator 
{
private:
	static bool isGreaterPrecedence(std::wstring a, std::wstring b);
	static bool isOperator(std::wstring op);

public:
	static double evaluate(std::wstring expression);
	static std::wstring infixToPostfix(std::wstring infix);
	static double postfixValue(std::wstring postfix);
};
