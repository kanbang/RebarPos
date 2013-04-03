//-----------------------------------------------------------------------------
//----- RebarPos.cpp : Implementation of RebarPos
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include "MgRebarPos.h"
#include "Marshal.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
RebarPos::RebarPos()
: Autodesk::AutoCAD::DatabaseServices::Entity(IntPtr(new CRebarPos()), true)
{
	mCalculatedProperties = gcnew RebarPos::CalculatedProperties();
}

RebarPos::RebarPos(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::Entity(unmanagedPointer, autoDelete)
{
	mCalculatedProperties = gcnew RebarPos::CalculatedProperties();
}

//*************************************************************************
// Properties
//*************************************************************************
Vector3d RebarPos::DirectionVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->DirectionVector());
}

Vector3d RebarPos::UpVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->UpVector());
}

Vector3d RebarPos::NormalVector::get()
{
	return Marshal::ToVector3d (GetImpObj()->NormalVector());
}

double RebarPos::Scale::get()
{
	return (GetImpObj()->Scale());
}
void RebarPos::Scale::set(double value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setScale(value));
}

double RebarPos::Width::get()
{
	return (GetImpObj()->Width());
}

double RebarPos::Height::get()
{
	return (GetImpObj()->Height());
}

void RebarPos::BasePoint::set(Point3d point)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setBasePoint(Marshal::FromPoint3d(point)));
}
Point3d RebarPos::BasePoint::get()
{
    return Marshal::ToPoint3d (GetImpObj()->BasePoint());
}

void RebarPos::NoteGrip::set(Point3d point)
{
  Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNoteGrip(Marshal::FromPoint3d(point)));
}
Point3d RebarPos::NoteGrip::get()
{
    return Marshal::ToPoint3d (GetImpObj()->NoteGrip());
}

void RebarPos::LengthGrip::set(Point3d point)
{
  Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setLengthGrip(Marshal::FromPoint3d(point)));
}
Point3d RebarPos::LengthGrip::get()
{
    return Marshal::ToPoint3d (GetImpObj()->LengthGrip());
}

void RebarPos::Pos::set(String^ value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setPos(Marshal::StringToWchar(value)));
}
String^ RebarPos::Pos::get()
{
    return Marshal::WcharToString(GetImpObj()->Pos());
}

void RebarPos::Note::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setNote(Marshal::StringToWchar(value)));
}
String^ RebarPos::Note::get()
{
    return Marshal::WcharToString(GetImpObj()->Note());
}

void RebarPos::Count::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setCount(Marshal::StringToWchar(value)));
}
String^ RebarPos::Count::get()
{
    return Marshal::WcharToString(GetImpObj()->Count());
}

void RebarPos::Diameter::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDiameter(Marshal::StringToWchar(value)));
}
String^ RebarPos::Diameter::get()
{
    return Marshal::WcharToString(GetImpObj()->Diameter());
}

void RebarPos::Spacing::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setSpacing(Marshal::StringToWchar(value)));
}
String^ RebarPos::Spacing::get()
{
    return Marshal::WcharToString(GetImpObj()->Spacing());
}

void RebarPos::IncludeInBOQ::set(bool value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setIncludeInBOQ(value ?  Adesk::kTrue :  Adesk::kFalse));
}
bool RebarPos::IncludeInBOQ::get()
{
    return (GetImpObj()->IncludeInBOQ() == Adesk::kTrue);
}

void RebarPos::Multiplier::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setMultiplier(value));
}
int RebarPos::Multiplier::get()
{
    return GetImpObj()->Multiplier();
}

void RebarPos::Display::set(RebarPos::DisplayStyle value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDisplay(static_cast<CRebarPos::DisplayStyle>(value)));
}
RebarPos::DisplayStyle RebarPos::Display::get()
{
	return static_cast<RebarPos::DisplayStyle>(GetImpObj()->Display());
}

void RebarPos::Shape::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setShape(Marshal::StringToWchar(value)));
}
String^ RebarPos::Shape::get()
{
    return Marshal::WcharToString(GetImpObj()->Shape());
}

void RebarPos::A::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setA(Marshal::StringToWchar(value)));
}
String^ RebarPos::A::get()
{
    return Marshal::WcharToString(GetImpObj()->A());
}

void RebarPos::B::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setB(Marshal::StringToWchar(value)));
}
String^ RebarPos::B::get()
{
    return Marshal::WcharToString(GetImpObj()->B());
}

void RebarPos::C::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setC(Marshal::StringToWchar(value)));
}
String^ RebarPos::C::get()
{
    return Marshal::WcharToString(GetImpObj()->C());
}

void RebarPos::D::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setD(Marshal::StringToWchar(value)));
}
String^ RebarPos::D::get()
{
    return Marshal::WcharToString(GetImpObj()->D());
}

void RebarPos::E::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setE(Marshal::StringToWchar(value)));
}
String^ RebarPos::E::get()
{
    return Marshal::WcharToString(GetImpObj()->E());
}

void RebarPos::F::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setF(Marshal::StringToWchar(value)));
}
String^ RebarPos::F::get()
{
    return Marshal::WcharToString(GetImpObj()->F());
}

String^ RebarPos::Length::get()
{
    return Marshal::WcharToString(GetImpObj()->Length());
}

void RebarPos::Detached::set(bool value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setDetached(value ?  Adesk::kTrue :  Adesk::kFalse));
}
bool RebarPos::Detached::get()
{
    return (GetImpObj()->Detached() == Adesk::kTrue);
}

String^ RebarPos::PosKey::get()
{
    return Marshal::WcharToString(GetImpObj()->PosKey());
}

array<PosShape::Shape^>^ RebarPos::Shapes::get()
{
	std::vector<CShape*> shapes = GetImpObj()->GetShapes();
	array<PosShape::Shape^>^ list = gcnew array<PosShape::Shape^>((int)shapes.size());

	int i = 0;
	for(ShapeListIt it = shapes.begin(); it != shapes.end(); it++)
	{
		list[i] = PosShape::Shape::FromNative(*it);
		i++;
	}

	return list;
}

RebarPos::CalculatedProperties^ RebarPos::CalcProperties::get()
{
	if(mCalculatedProperties->mGeneration < GetImpObj()->CalcProps().Generation)
		mCalculatedProperties = gcnew RebarPos::CalculatedProperties(GetImpObj()->CalcProps()); 
	return mCalculatedProperties; 
}

Autodesk::AutoCAD::DatabaseServices::ObjectId RebarPos::GroupIdForDisplay::get()
{
	return Marshal::ToObjectId (GetImpObj()->GroupIdForDisplay());
}
void RebarPos::GroupIdForDisplay::set(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
{
	Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setGroupIdForDisplay(Marshal::FromObjectId(value)));
}

//*************************************************************************
// Methods
//*************************************************************************
void RebarPos::Update()
{
	GetImpObj()->Update();
}

RebarPos::HitTestResult RebarPos::HitTest(Point3d pt)
{
	return static_cast<RebarPos::HitTestResult>(GetImpObj()->HitTest(Marshal::FromPoint3d(pt)));
}

void RebarPos::TextBox([Out] Point3d% minPoint, [Out] Point3d% maxPoint)
{
	AcGePoint3d pmin;
	AcGePoint3d pmax;
	GetImpObj()->TextBox(pmin, pmax);
	minPoint = Point3d(pmin.x, pmin.y, 0.0);
	maxPoint = Point3d(pmax.x, pmax.y, 0.0);
}

//*************************************************************************
// Static Methods
//*************************************************************************
bool RebarPos::GetTotalLengths(String^ formula, int fieldCount, PosGroup::DrawingUnits inputUnit, String^ a, String^ b, String^ c, String^ d, String^ e, String^ f, String^ diameter, [Out] double% minLength, [Out] double% maxLength, [Out] bool% isVar)
{
	double len1;
	double len2;
	bool var;

	bool check = CRebarPos::GetTotalLengths(Marshal::StringToWchar(formula), fieldCount, static_cast<CPosGroup::DrawingUnits>(inputUnit), Marshal::StringToWchar(a), Marshal::StringToWchar(b), Marshal::StringToWchar(c), Marshal::StringToWchar(d), Marshal::StringToWchar(e), Marshal::StringToWchar(f), Marshal::StringToWchar(diameter), len1, len2, var);

	minLength = len1;
	maxLength = len2;
	isVar = var;

	return check;
}

double RebarPos::ConvertLength(double length, PosGroup::DrawingUnits fromUnit, PosGroup::DrawingUnits toUnit)
{
	return CRebarPos::ConvertLength(length, static_cast<CPosGroup::DrawingUnits>(fromUnit), static_cast<CPosGroup::DrawingUnits>(toUnit));
}

double RebarPos::BendingRadius(double d)
{
	return CRebarPos::BendingRadius(d);
}
