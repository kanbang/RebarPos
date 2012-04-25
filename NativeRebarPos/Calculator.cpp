//-----------------------------------------------------------------------------
//----- Calculator.cpp : Implementation of CCalculator
//-----------------------------------------------------------------------------

#include "Calculator.h"

// Example usage
// std::string infix = "( ( ( -1 + 3 ) / 4 ) * -10 )";
// double value = CCalculator::evaluate(infix);
// std::string postfix = CCalculator::infixToPostfix(infix);
// double value = CCalculator::postfixValue(postfix);

bool CCalculator::isGreaterPrecedence(std::wstring a, std::wstring b) 
{
	const std::wstring ops = L"(*/-+";
	return wcschr(ops.c_str(), a[0]) <= wcschr(ops.c_str(), b[0]);
}

bool CCalculator::isOperator(std::wstring op) 
{
	return std::wstring(L"(*/+-").find(op) != std::wstring::npos;
}

double CCalculator::evaluate(std::wstring expression) 
{
	return postfixValue(infixToPostfix(expression));
}

std::wstring CCalculator::infixToPostfix(std::wstring infix) 
{
	std::wstring postfix;
	std::stack<std::wstring> ops;
	std::wstringstream ss(infix);
	std::wstring token;
	while(ss >> token) { //loop through all operands and operators
		if(token == L")") {
			std::wstring val;
			/* pop operator stack all the way back until we 
			   find the corrisponding opening operator */
			while((val = ops.top()) != L"(") { 
				postfix.append(val + L" ");
				ops.pop();
			}
			ops.pop(); //pop the opening operator off the stack
		}
		else if(isOperator(token)) {
			/* While the operator is of lesser precedence 
			   than the one beneath it, pop until it's not */
			while(!ops.empty() &&
				 isGreaterPrecedence(ops.top(), token) &&
				 ops.top() != L"(") {
					postfix.append(ops.top() + L" ");
					ops.pop();
			}
			ops.push(token); //add the operator to the stack
		}
		else {
			//if it's not an operator, then just append it to the output string
			postfix.append(token + L" ");
		}
	}

	// append everything else left on the stack
	while(!ops.empty()) {
		postfix.append(ops.top() + L" ");
		ops.pop();
	}
	return postfix;
}

double CCalculator::postfixValue(std::wstring postfix) 
{
	std::stack <double> vals;
	std::wstring token;
	std::wstringstream ss(postfix);
	while(ss >> token) {
		// if it's an operator, then evaluate it with the two most 
		// top operands on the stack and push the result on the stack
		if(isOperator(token)) {
			double b = vals.top(); vals.pop();
			double a = vals.top(); vals.pop();
			switch(token[0]) {
				case '*': vals.push(a * b); break;
				case '/': vals.push(a / b); break;
				case '+': vals.push(a + b); break;
				case '-': vals.push(a - b); break;
			}
		}
		else {
			vals.push(_wtof(token.c_str())); // push operand to top of stack
		}
	}

	// final value is the last value on the stack
	return vals.top();
}
