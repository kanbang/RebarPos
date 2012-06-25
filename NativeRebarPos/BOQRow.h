#pragma once

#include <string>

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

struct DLLIMPEXP CBOQRow
{
	Adesk::Int32 pos;
	Adesk::Int32 count;
	double diameter;
	double length1;
	double length2;
	Adesk::Boolean isVarLength;
	AcDbHardPointerId shapeId;
	Adesk::Boolean isEmpty;
	std::wstring a;
	std::wstring b;
	std::wstring c;
	std::wstring d;
	std::wstring e;
	std::wstring f;

	CBOQRow() :
	pos(0), count(0), diameter(0), length1(0), length2(0), isVarLength(Adesk::kFalse), shapeId(AcDbObjectId::kNull), isEmpty(Adesk::kFalse), a(), b(), c(), d(), e(), f()
	{ 
		
	}

	CBOQRow(const Adesk::Int32 Pos, const Adesk::Int32 Count, const double Diameter, const double Length1, const double Length2, const Adesk::Boolean IsVarLength, const AcDbObjectId ShapeId,
		const ACHAR* A, const ACHAR* B, const ACHAR* C, const ACHAR* D, const ACHAR* E, const ACHAR* F) :
		pos(Pos), count(Count), diameter(Diameter), length1(Length1), length2(Length2), isVarLength(IsVarLength), shapeId(ShapeId), isEmpty(Adesk::kFalse), 
		a(A), b(B), c(C), d(D), e(E), f(F)
	{ 
		
	}

		CBOQRow(const Adesk::Int32 Pos) :
			pos(Pos), count(0), diameter(0), length1(0), length2(0), isVarLength(Adesk::kFalse), shapeId(AcDbObjectId::kNull), isEmpty(Adesk::kTrue), 
			a(), b(), c(), d(), e(), f()
	{ 
		
	}
};
