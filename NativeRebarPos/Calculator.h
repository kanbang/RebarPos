//-----------------------------------------------------------------------------
//----- Calculator.h : Declaration of the Calculator
//-----------------------------------------------------------------------------
#pragma once

#include <string>
#include <vector>
#include <stack>
#include <queue>
#include <math.h>

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

class DLLIMPEXP Calculator
{
public:
	enum Errors
	{
		eOK = 0,
		eParenthesisMisMatch = 1,
		eNotAnOperator = 2,
		eUnknownOperator = 3,
		eInvalidFormula = 4
	};

protected:
	struct Token
	{
	public:
		wchar_t Op;
		double Value;
		bool IsOp;
		bool IsLeftAssoc;
		int Rank;

	public:
		bool operator==(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Op == other.Op;
		}
		bool operator!=(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Op != other.Op;
		}
		bool operator<(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Rank < other.Rank;
		}
		bool operator>(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Rank > other.Rank;
		}
		bool operator<=(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Rank <= other.Rank;
		}
		bool operator>=(const Token &other) const 
		{
			if(!IsOp || !other.IsOp)
				throw eNotAnOperator;
			return Rank >= other.Rank;
		}

	public:
		const double Eval(const double a, const double b) const
		{
			if(!IsOp)
				throw eNotAnOperator;

			switch(Op)
			{
				case L'+': return a + b; break;
				case L'-': return a - b; break;
				case L'*': return a * b; break;
				case L'/': return a / b; break;
				case L'^': return pow(a, b); break;
				default:
					throw eUnknownOperator;
			}
		}

	public:
		Token(const wchar_t op)
		{
			IsOp = true;
			Op = op;
			IsLeftAssoc = (op == L'+' || op == L'-' || op == L'*' || op == L'/');
			Rank = ((op == L'^') ? 2 : ((op == L'*' || op == L'/') ? 1 : 0));

			Value = 0.0;
		}
		Token(const double value)
		{
			IsOp = false;
			Op = L'\0';
			IsLeftAssoc = false;
			Rank = 0;

			Value = value;
		}
		Token(const std::wstring value)
		{
			IsOp = false;
			Op = L'\0';
			IsLeftAssoc = false;
			Rank = 0;

			wchar_t* endstr;
			Value = wcstod(value.c_str(), &endstr);
		}
	};

private:
	Calculator() { }
	Calculator(Calculator const&) { }
	void operator=(Calculator const&) { }

protected:
	static bool IsOperator(const wchar_t c);
	static bool IsNumeric(const wchar_t c);

	static std::queue<Token> Tokenize(const std::wstring str);
	static std::queue<Token> InfixToRPN(std::queue<Calculator::Token> tokens);

public:
	static double Evaluate(const std::wstring formula);
	static bool IsValid(const std::wstring formula);
};
