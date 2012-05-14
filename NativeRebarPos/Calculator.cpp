//-----------------------------------------------------------------------------
//----- Calculator.cpp : Implementation of Calculator
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "Calculator.h"

bool Calculator::IsOperator(const wchar_t c)
{
	std::wstring ops = L"+-*/^()";
	return ops.find(c) != std::wstring::npos;
}

bool Calculator::IsNumeric(const wchar_t c)
{
	std::wstring nums = L"1234567890.";
	return nums.find(c) != std::wstring::npos;
}

std::queue<Calculator::Token> Calculator::Tokenize(const std::wstring str)
{
	std::queue<Calculator::Token> tokens;
	std::wstring token;

	for(std::wstring::const_iterator it = str.begin(); it != str.end(); it++)
	{
		wchar_t c = *it;
		if(IsNumeric(c))
		{
			token += c;
		}
		else if(IsOperator(c))
		{
			if(!token.empty())
			{
				tokens.push(Calculator::Token(token));
				token.clear();
			}
			tokens.push(Calculator::Token(c));
		}
	}

	if(!token.empty())
	{
		tokens.push(Calculator::Token(token));
	}

	return tokens;
}

std::queue<Calculator::Token> Calculator::InfixToRPN(std::queue<Calculator::Token> tokens)
{
	std::queue<Calculator::Token> output;
	std::stack<Calculator::Token> stack;

	while(!tokens.empty())
	{
		Calculator::Token token = tokens.front(); tokens.pop();

		if(!token.IsOp)
		{
			// If the token is a number, then add it to the output queue.
			output.push(token);
		}
		else if(token.Op == L'(')
		{
			// If the token is a left parenthesis, then push it onto the stack.
			stack.push(token);
		}
		else if(token.Op == L')')
		{
			if(stack.empty())
				throw eParenthesisMisMatch;

			// Pop operators from the stack to the output queue until we find opening parenthesis
			while(stack.top().Op != L'(')
			{
				output.push(stack.top()); stack.pop();
			}

			if(stack.empty() || stack.top().Op != L'(')
				throw eParenthesisMisMatch;

			// Pop left parenthesis
			stack.pop();
		}
		else
		{
			Token o1 = token;
			Token o2(L'*');
			if(!stack.empty()) o2 = stack.top();
			// Operator (o1)
			// While there is an operator token, o2, at the top of the stack, and
			//   either o1 is left-associative and its precedence is less than or equal to that of o2,
			//   or o1 is right-associative and its precedence is less than that of o2,
			//   pop o2 off the stack, onto the output queue;
		    // push o1 onto the stack.
			while(!stack.empty() && 
				stack.top().Op != L'(' && stack.top().Op != L')' &&
				((token.IsLeftAssoc && (token <= stack.top())) || (!token.IsLeftAssoc && (token < stack.top()))))
				{
					output.push(stack.top()); stack.pop();
				}
			stack.push(token);
		}
	}

	// Pop remaining operators to the output queue.
	while(!stack.empty())
	{
		if((stack.top().Op == L'(' || stack.top().Op == L')'))
			throw eParenthesisMisMatch;

		output.push(stack.top()); stack.pop();
	}

	return output;
}

double Calculator::Evaluate(const std::wstring formula)
{
	std::queue<Calculator::Token> infixtokens = Tokenize(formula);
	std::queue<Calculator::Token> rpntokens = InfixToRPN(infixtokens);
	std::stack<double> vals;

	while(!rpntokens.empty())
	{
		Calculator::Token token = rpntokens.front(); rpntokens.pop();
		if(token.IsOp)
		{
			if(vals.size() < 2)
				throw eInvalidFormula;
			double op2 = vals.top(); vals.pop();
			double op1 = vals.top(); vals.pop();
			double res = token.Eval(op1, op2);
			vals.push(res);
		}
		else
		{
			vals.push(token.Value);
		}
	}

	if(vals.size() != 1)
		throw eInvalidFormula;

	return vals.top();
}

bool Calculator::IsValid(const std::wstring formula)
{
	try
	{
		Evaluate(formula);
		return true;
	}
	catch(...)
	{
		return false;
	}
}