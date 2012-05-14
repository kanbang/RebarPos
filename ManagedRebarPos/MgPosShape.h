//-----------------------------------------------------------------------------
//----- PosShape.h : Declaration of PosShape
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\Shape.h"
#include "..\NativeRebarPos\PosShape.h"
#include "Marshal.h"

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ 
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CPosShape")]
        public ref class PosShape :  public Autodesk::AutoCAD::DatabaseServices::DBObject
        {
		public:
			ref struct ShapeLine;
			ref struct ShapeArc;
			ref struct ShapeText;

			ref struct Shape abstract
			{

			public:
				property Autodesk::AutoCAD::Colors::Color^ Color;

			protected:
				Shape()
				{
					Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, 0);
				}
				Shape(Autodesk::AutoCAD::Colors::Color^ color)
				{
					Color = color;
				}

			private:
				Shape(Shape%) { }
				void operator=(Shape%) { }

			internal:
				virtual CShape* ToNative(void) = 0;

			internal:
				static Shape^ FromNative(const CShape* shape)
				{
					switch(shape->type)
					{
					case CShape::Line:
						return FromNative(dynamic_cast<const CShapeLine*>(shape));
						break;
					case CShape::Arc:
						return FromNative(dynamic_cast<const CShapeArc*>(shape));
						break;
					case CShape::Text:
						return FromNative(dynamic_cast<const CShapeText*>(shape));
						break;
					default:
						throw gcnew Exception("Unknown shape type");
					}
				}
				static ShapeLine^ FromNative(const CShapeLine* line)
				{
					return gcnew ShapeLine(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, line->color),
						line->x1, line->y1, line->x2, line->y2);
				}
				static ShapeArc^ FromNative(const CShapeArc* arc)
				{
					return gcnew ShapeArc(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, arc->color),
						arc->x, arc->y, arc->r, arc->startAngle, arc->endAngle);
				}
				static ShapeText^ FromNative(const CShapeText* text)
				{
					return gcnew ShapeText(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, text->color),
						text->x, text->y, text->height, Marshal::WcharToString(text->text), 
						static_cast<TextHorizontalMode>(text->horizontalAlignment), static_cast<TextVerticalMode>(text->verticalAlignment));
				}
			};

			ref struct ShapeLine : Shape
			{
			public:
				property double X1;
				property double Y1;
				property double X2;
				property double Y2;

			public:
				ShapeLine() : Shape()
				{
					;
				}

				ShapeLine(Autodesk::AutoCAD::Colors::Color^ color, double x1, double y1, double x2, double y2) : Shape(color)
				{
					X1 = x1;
					Y1 = y1;
					X2 = x2;
					Y2 = y2;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeLine(Color->ColorIndex, X1, Y1, X2, Y2);
				}
			};

			ref struct ShapeArc : Shape
			{
			public:
				property double X;
				property double Y;
				property double R;
				property double StartAngle;
				property double EndAngle;

			public:
				ShapeArc() : Shape()
				{
					;
				}

				ShapeArc(Autodesk::AutoCAD::Colors::Color^ color, double x, double y, double r, double startAngle, double endAngle) : Shape(color)
				{
					X = x;
					Y = y;
					R = r;
					StartAngle = startAngle;
					EndAngle = endAngle;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeArc(Color->ColorIndex, X, Y, R, StartAngle, EndAngle);
				}
			};

			ref struct ShapeText : Shape
			{
			public:
				property double X;
				property double Y;
				property double Height;
				property String^ Text;
				property TextHorizontalMode HorizontalAlignment;
				property TextVerticalMode VerticalAlignment;

			public:
				ShapeText() : Shape()
				{
					;
				}
				ShapeText(Autodesk::AutoCAD::Colors::Color^ color, double x, double y, double height, String^ text, TextHorizontalMode horizontalAlignment, TextVerticalMode verticalAlignment) : Shape(color)
				{
					X = x;
					Y = y;
					Height = height;
					Text = text;
					HorizontalAlignment = horizontalAlignment;
					VerticalAlignment = verticalAlignment;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeText(Color->ColorIndex, X, Y, Height, (wchar_t*)Marshal::StringToWchar(Text),
						static_cast<AcDb::TextHorzMode>(HorizontalAlignment), static_cast<AcDb::TextVertMode>(VerticalAlignment));
				}
			};

		public:
			ref class ShapeCollection
			{
			private:
				PosShape^ m_Parent;

			private:
				ShapeCollection() { }
				ShapeCollection(ShapeCollection%) { }
				void operator=(ShapeCollection%) { }

			internal:
				ShapeCollection(PosShape^ parent);

			public:
				void AddLine(double x1, double y1, double x2, double y2, Autodesk::AutoCAD::Colors::Color^ color);
				void AddArc(double x, double y, double r, double startAngle, double endAngle, Autodesk::AutoCAD::Colors::Color^ color);
				void AddText(double x, double y, double height, String^ text, Autodesk::AutoCAD::Colors::Color^ color, TextHorizontalMode horizontalAlignment, TextVerticalMode verticalAlignment);
				property int Count { int get(); }
				property Shape^ default[int] { Shape^ get(int index); void set(int index, Shape^ value); }
				void Remove(int index);
				void Clear();
			};

		protected:
			ShapeCollection^ m_Shapes;

        public:
            PosShape();

        internal:
            PosShape(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CPosShape* GetImpObj()
            {
                return static_cast<CPosShape*>(UnmanagedObject.ToPointer());
            }

        public:
			property ShapeCollection^ Items { ShapeCollection^ get(); }

			property String^ Name           { String^ get(); void set(String^ value); }
			property int Fields             { int get(); void set(int value); }
			property String^ Formula        { String^ get(); void set(String^ value); }
			property String^ FormulaBending { String^ get(); void set(String^ value); }

			property int Priority { int get(); void set(int value); }

		public:
			property static String^ TableName        { String^ get(); }
        };
    }

}