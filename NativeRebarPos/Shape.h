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

struct DLLIMPEXP CShape
{
	enum ShapeType
	{ 
		Line = 0,
		Arc = 1,
		Text = 2,
		Circle = 3,
		Ellipse = 4,
	};

	CShape::ShapeType type;
	Adesk::UInt16 color;
	Adesk::Boolean visible;

	virtual CShape* clone() const = 0;

protected:
	CShape(const CShape::ShapeType Type, const Adesk::UInt16 Color = 0, const Adesk::Boolean Visible = Adesk::kTrue) : type(Type), color(Color), visible(Visible)
	{ }
};

struct DLLIMPEXP CShapeLine : CShape
{
	double x1;
	double y1;
	double x2;
	double y2;

	CShapeLine();
	CShapeLine(const Adesk::UInt16 Color, const double X1, const double Y1, const double X2, const double Y2, const Adesk::Boolean Visible = Adesk::kTrue);

	virtual CShapeLine* clone() const;
};

struct DLLIMPEXP CShapeArc : CShape
{
	double x;
	double y;
	double r;
	double startAngle;
	double endAngle;

	CShapeArc();

	CShapeArc(const Adesk::UInt16 Color, const double X, const double Y, const double R, const double StartAngle, const double EndAngle, const Adesk::Boolean Visible = Adesk::kTrue);

	virtual CShapeArc* clone() const;
};

struct DLLIMPEXP CShapeCircle : CShape
{
	double x;
	double y;
	double r;

	CShapeCircle();

	CShapeCircle(const Adesk::UInt16 Color, const double X, const double Y, const double R, const Adesk::Boolean Visible = Adesk::kTrue);

	virtual CShapeCircle* clone() const;
};

struct DLLIMPEXP CShapeEllipse : CShape
{
	double x;
	double y;
	double r;
	double width;
	double height;

	CShapeEllipse();

	CShapeEllipse(const Adesk::UInt16 Color, const double X, const double Y, const double Width, const double Height, const Adesk::Boolean Visible = Adesk::kTrue);

	virtual CShapeEllipse* clone() const;
};

struct DLLIMPEXP CShapeText : CShape
{
	double x;
	double y;
	std::wstring font;
	double height;
	double width;
	std::wstring text;
	AcDb::TextHorzMode horizontalAlignment;
	AcDb::TextVertMode verticalAlignment;

	CShapeText();

	CShapeText(const Adesk::UInt16 Color, const double X, const double Y, const double Height, const double Width, const std::wstring& Text, const std::wstring& Font, const AcDb::TextHorzMode HorizontalAlignment, const AcDb::TextVertMode VerticalAlignment, const Adesk::Boolean Visible = Adesk::kTrue);

	void SetText(const std::wstring& Text);
	void SetText(const wchar_t* Text);

	void SetFont(const std::wstring& Font);
	void SetFont(const wchar_t* Font);

	virtual CShapeText* clone() const;
};
