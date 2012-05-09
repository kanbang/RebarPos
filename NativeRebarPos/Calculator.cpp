//-----------------------------------------------------------------------------
//----- Calculator.cpp : Implementation of CCalculator
//-----------------------------------------------------------------------------

#include "Calculator.h"

// -------------------------------------------- 
// Infix arithmetic calculator.                 
// Supports +, -, *, / and paranthesis          
//                                              
// CalcInfix("1 + 3 * (4 + 3)")
// returns 22.0                                 
// -------------------------------------------- 
double CCalculator::CalcInfix(std::wstring str)
{
	try
	{
		return CalcRPN(InfixToRPN(str));
	}
	catch(...)
	{
		return 0.0;
	}
}

// -------------------------------------------- 
// Reverse polish notation arithmetic           
// calculator.                                  
//                                              
// CalcRPN("1 3 4 3 + * +")                    
// returns 22.0                                 
// -------------------------------------------- 
double CCalculator::CalcRPN(std::wstring str)
{
	std::stack<std::wstring> stack;
	std::wstring token;

	for(std::wstring::iterator it = str.begin(); it != str.end(); it++)
	{
		wchar_t c = *it;
		if(IsNumericChar(c))
		{
			token = token + c;
		}
		else
		{
			if(token != L"")
			{
				stack.push(token);
				token = L"";
			}
		}

		if(IsOp(c))
		{
			double val = 0.0;
			if(stack.size() >= 2)
			{
				double a = _wtof(stack.top().c_str()); stack.pop();
				double b = _wtof(stack.top().c_str()); stack.pop();
				switch(c) 
				{
					case '*': val = a * b; break;
					case '/': val = a / b; break;
					case '+': val = a + b; break;
					case '-': val = a - b; break;
				}
			}
			std::wstringstream sval;
			sval << val;
			str = sval.str();
			stack.push(sval.str());
		}
	}
	if(token != L"")
	{
		return _wtof(token.c_str());
	}
	else if(stack.empty())
	{
		return 0.0;
	}
	else
	{
		token = stack.top();
		return _wtof(token.c_str());
	}
}

// -------------------------------------------- 
// Converts infix expression to reverse polish  
// notation.                                    
//                                              
// InfixToRPN ("1 + 3 * (4 + 3)")               
// returns "1 3 4 3 + * +"                      
// -------------------------------------------- 
std::wstring CCalculator::InfixToRPN(std::wstring str)
{
	std::stack<wchar_t> stack;
	std::vector<std::wstring> output;
	std::wstring token;

	for(std::wstring::iterator it = str.begin(); it != str.end(); it++)
	{
		wchar_t c = *it;
		if(IsNumericChar(c))
		{
			token = token + c;
		}
		else
		{
			if(token != L"")
			{
				output.push_back(token);
				token = L"";
			}
		}

		if(IsOp(c))
		{
			if(!stack.empty())
			{
				while(IsOp(stack.top()) && (OpPrecendence(c) <= OpPrecendence(stack.top())))
				{
					output.push_back(std::wstring(&stack.top()));
					stack.pop();
				}
			}
			stack.push(c);
		}
		if(IsLeftParen(c))
		{
			stack.push(c);
		}
	    if(IsRightParen(c))
		{
			if(!stack.empty())
			{
				while(!IsLeftParen(stack.top()))
				{
					output.push_back(std::wstring(&stack.top()));
					stack.pop();
				}
			}
			stack.pop();
		}
	}

	if(token != L"") output.push_back(token);
	while(!stack.empty())
	{
		token = stack.top(); stack.pop();
		output.push_back(token); // prepend?
	}

	std::wstring outstr;
	for(std::vector<std::wstring>::iterator it = output.begin(); it != output.end(); it++)
	{
		token = *it;
		outstr = outstr + token + L" ";
	}
	outstr = outstr.substr(0, outstr.length() - 1);
	return outstr;
}

// -------------------------------------------- 
// Determines precendence between operators.    
// -------------------------------------------- 
int CCalculator::OpPrecendence(wchar_t op)
{
	if(op == '+' || op == '-')
		return 0;
	else
		return 1;
}

// -------------------------------------------- 
// Determines if the given character is an      
// operator.                                    
// -------------------------------------------- 
bool CCalculator::IsOp(wchar_t op)
{
	return std::wstring(L"+-*/").find(op) != std::wstring::npos;
}

// -------------------------------------------- 
// Determines if the given character is an      
// opening paranthesis.                         
// -------------------------------------------- 
bool CCalculator::IsLeftParen(wchar_t op)
{
	return op == '(';
}

// -------------------------------------------- 
// Determines if the given character is a       
// closing paranthesis.                         
// -------------------------------------------- 
bool CCalculator::IsRightParen(wchar_t op)
{
	return op == ')';
}

// -------------------------------------------- 
// Determines if the given character is a       
// number.                                      
// -------------------------------------------- 
bool CCalculator::IsNumericChar(wchar_t op)
{
	return std::wstring(L"1234567890.").find(op) != std::wstring::npos;
}
