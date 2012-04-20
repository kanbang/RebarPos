//-----------------------------------------------------------------------------
//----- PosShape.h : Declaration of PosShape
//-----------------------------------------------------------------------------

#pragma once

#include <tchar.h>
#include <gcroot.h>
#include "mgdinterop.h"

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
			ref struct Shape
			{
			public:
				property Autodesk::AutoCAD::Colors::Color^ Color;

			protected:
				Shape(void)
				{
					Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, 0);
				}

			internal:
				CShape* ToCShape(void)
				{
					if(this->GetType() == ShapeLine::typeid)
					{
						ShapeLine^ rshape = static_cast<ShapeLine^>(this);
						CShapeLine* line = new CShapeLine();
						line->color = rshape->Color->ColorIndex;
						line->x1 = rshape->X1;
						line->y1 = rshape->Y1;
						line->x2 = rshape->X2;
						line->y2 = rshape->Y1;
						return line;
					}
					else if(this->GetType() == ShapeArc::typeid)
					{
						ShapeArc^ rshape = static_cast<ShapeArc^>(this);
						CShapeArc* arc = new CShapeArc();
						arc->color = rshape->Color->ColorIndex;
						arc->x = rshape->X;
						arc->y = rshape->Y;
						arc->r = rshape->R;
						arc->startAngle = rshape->StartAngle;
						arc->endAngle = rshape->EndAngle;
						return arc;
					}
					else if(this->GetType() == ShapeText::typeid)
					{
						ShapeText^ rshape = static_cast<ShapeText^>(this);
						CShapeText* text = new CShapeText();
						text->color = rshape->Color->ColorIndex;
						text->x = rshape->X;
						text->y = rshape->Y;
						text->height = rshape->Height;
						acutUpdString(StringToWchar(rshape->Text), text->text);
						return text;
					}
					else
					{
						throw gcnew Exception("Unknown shape type");
					}
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
				ShapeLine(void) : Shape()
				{
					;
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
				ShapeArc(void) : Shape()
				{
					;
				}
			};

			ref struct ShapeText : Shape
			{
			public:
				property double X;
				property double Y;
				property double Height;
				property String^ Text;

			public:
				ShapeText(void) : Shape()
				{
					;
				}
			};

		public:
			ref class ShapeCollection
			{
			private:
				PosShape^ m_Parent;

			internal:
				ShapeCollection(PosShape^ parent);

			public:
				void AddLine(double x1, double y1, double x2, double y2, Autodesk::AutoCAD::Colors::Color^ color);
				void AddArc(double x, double y, double r, double startAngle, double endAngle, Autodesk::AutoCAD::Colors::Color^ color);
				void AddText(double x, double y, double height, String^ text, Autodesk::AutoCAD::Colors::Color^ color);
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

			property int Fields { int get(); void set(int value); }

			property String^ Formula        { String^ get(); void set(String^ value); }
			property String^ FormulaBending { String^ get(); void set(String^ value); }

		public:
			property static String^ TableName        { String^ get(); }
			property static Autodesk::AutoCAD::DatabaseServices::ObjectId Dictionary { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); }

		public:
			static Autodesk::AutoCAD::DatabaseServices::ObjectId CreateDefault();
        };
    }

}