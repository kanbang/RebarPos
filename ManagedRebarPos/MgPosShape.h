//-----------------------------------------------------------------------------
//----- PosShape.h : Declaration of PosShape
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\Shape.h"
#include "..\NativeRebarPos\PosShape.h"
#include "Marshal.h"

using namespace System;
using namespace System::Drawing;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;
using namespace Autodesk::AutoCAD::GraphicsInterface;
using namespace Autodesk::AutoCAD::GraphicsSystem;

namespace OZOZ 
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CPosShape")]
		public ref class PosShape :  public Autodesk::AutoCAD::GraphicsInterface::Drawable
        {
		public:
			ref struct ShapeLine;
			ref struct ShapeArc;
			ref struct ShapeCircle;
			ref struct ShapeText;

			ref struct Shape abstract
			{
			public:
				property Autodesk::AutoCAD::Colors::Color^ Color;
				property bool Visible;

			public:
				virtual Shape^ Clone() = 0;

			protected:
				Shape()
				{
					Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, 0);
					Visible = true;
				}
				Shape(Autodesk::AutoCAD::Colors::Color^ color, bool visible)
				{
					Color = color;
					Visible = visible;
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
					case CShape::Circle:
						return FromNative(dynamic_cast<const CShapeCircle*>(shape));
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
						line->x1, line->y1, line->x2, line->y2, line->visible == Adesk::kTrue);
				}
				static ShapeArc^ FromNative(const CShapeArc* arc)
				{
					return gcnew ShapeArc(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, arc->color),
						arc->x, arc->y, arc->r, arc->startAngle, arc->endAngle, arc->visible == Adesk::kTrue);
				}
				static ShapeCircle^ FromNative(const CShapeCircle* circle)
				{
					return gcnew ShapeCircle(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, circle->color),
						circle->x, circle->y, circle->r, circle->visible == Adesk::kTrue);
				}
				static ShapeText^ FromNative(const CShapeText* text)
				{
					return gcnew ShapeText(
						Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, text->color),
						text->x, text->y, text->height, text->width, Marshal::WstringToString(text->text), Marshal::WstringToString(text->font), 
						static_cast<TextHorizontalMode>(text->horizontalAlignment), static_cast<TextVerticalMode>(text->verticalAlignment),
						text->visible == Adesk::kTrue);
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
				virtual Shape^ Clone() override
				{
					return gcnew ShapeLine(Color, X1, Y1, X2, Y2, Visible);
				}

			public:
				ShapeLine() : Shape()
				{
					;
				}

				ShapeLine(Autodesk::AutoCAD::Colors::Color^ color, double x1, double y1, double x2, double y2, bool visible) : Shape(color, visible)
				{
					X1 = x1;
					Y1 = y1;
					X2 = x2;
					Y2 = y2;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeLine(Color->ColorIndex, X1, Y1, X2, Y2, Visible ? Adesk::kTrue : Adesk::kFalse);
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
				virtual Shape^ Clone() override
				{
					return gcnew ShapeArc(Color, X, Y, R, StartAngle, EndAngle, Visible);
				}

			public:
				ShapeArc() : Shape()
				{
					;
				}

				ShapeArc(Autodesk::AutoCAD::Colors::Color^ color, double x, double y, double r, double startAngle, double endAngle, bool visible) : Shape(color, visible)
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
					return new CShapeArc(Color->ColorIndex, X, Y, R, StartAngle, EndAngle, Visible ? Adesk::kTrue : Adesk::kFalse);
				}
			};

			ref struct ShapeCircle : Shape
			{
			public:
				property double X;
				property double Y;
				property double R;

			public:
				virtual Shape^ Clone() override
				{
					return gcnew ShapeCircle(Color, X, Y, R, Visible);
				}

			public:
				ShapeCircle() : Shape()
				{
					;
				}

				ShapeCircle(Autodesk::AutoCAD::Colors::Color^ color, double x, double y, double r, bool visible) : Shape(color, visible)
				{
					X = x;
					Y = y;
					R = r;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeCircle(Color->ColorIndex, X, Y, R, Visible ? Adesk::kTrue : Adesk::kFalse);
				}
			};

			ref struct ShapeText : Shape
			{
			public:
				property double X;
				property double Y;
				property double Height;
				property double Width;
				property String^ Text;
				property String^ Font;
				property TextHorizontalMode HorizontalAlignment;
				property TextVerticalMode VerticalAlignment;

			public:
				virtual Shape^ Clone() override
				{
					return gcnew ShapeText(Color, X, Y, Height, Width, Text, Font, HorizontalAlignment, VerticalAlignment, Visible);
				}

			public:
				ShapeText() : Shape()
				{
					;
				}
				ShapeText(Autodesk::AutoCAD::Colors::Color^ color, double x, double y, double height, double width, String^ text, String^ font, TextHorizontalMode horizontalAlignment, TextVerticalMode verticalAlignment, bool visible) : Shape(color, visible)
				{
					X = x;
					Y = y;
					Height = height;
					Width = width;
					Text = text;
					Font = font;
					HorizontalAlignment = horizontalAlignment;
					VerticalAlignment = verticalAlignment;
				}

			internal:
				virtual CShape* ToNative(void) override
				{
					return new CShapeText(Color->ColorIndex, X, Y, Height, Width, Marshal::StringToWstring(Text),  Marshal::StringToWstring(Font),
						static_cast<AcDb::TextHorzMode>(HorizontalAlignment), static_cast<AcDb::TextVertMode>(VerticalAlignment), Visible ? Adesk::kTrue : Adesk::kFalse);
				}
			};

		public:
			ref class ShapeCollection : public System::Collections::Generic::IList<Shape^>
			{
			public:
				ref class ShapeCollectionIterator : public System::Collections::Generic::IEnumerator<Shape^>
				{
				private:
					ShapeCollection^ m_Parent;
					int index;

				private:
					ShapeCollectionIterator() { }
					ShapeCollectionIterator(ShapeCollectionIterator%) { }
					void operator=(ShapeCollectionIterator%) { }

				internal:
					ShapeCollectionIterator(ShapeCollection^ parent) 
					{ 
						m_Parent = parent; 
						index = -1; 
					}

				protected:
					~ShapeCollectionIterator() { this->!ShapeCollectionIterator(); }
					!ShapeCollectionIterator() { index = -1; }

				public:
					virtual bool MoveNext(void) 
					{
						if(index == m_Parent->Count - 1)
						{
							return false;
						}
						else
						{
							index++;
							return true;
						}
					}
					virtual void Reset(void) { index = -1; }
					property Shape^ Current 
					{ 
						virtual Shape^ get() = System::Collections::Generic::IEnumerator<Shape^>::Current::get { return m_Parent[index]; } 
					}

				protected:
					property System::Object^ Current2
					{ 
						virtual System::Object^ get() = System::Collections::IEnumerator::Current::get { return m_Parent[index]; } 
					}
				};

			private:
				PosShape^ m_Parent;

			private:
				ShapeCollection() { }
				ShapeCollection(ShapeCollection%) { }
				void operator=(ShapeCollection%) { }

			internal:
				ShapeCollection(PosShape^ parent) { m_Parent = parent; }

			public:
				virtual void Add(Shape^ value) { m_Parent->GetImpObj()->AddShape(value->ToNative()); }
				virtual void Insert(int index, Shape^ value) { m_Parent->GetImpObj()->InsertShape(index, value->ToNative()); }
				virtual void RemoveAt(int index) { m_Parent->GetImpObj()->RemoveShape(index); }
				virtual void Clear() { m_Parent->GetImpObj()->ClearShapes(); }
				virtual System::Collections::Generic::IEnumerator<Shape^>^ GetEnumerator()
				{
					return gcnew ShapeCollectionIterator(this);
				}

			public:
				property int Count { virtual int get() { return (int)m_Parent->GetImpObj()->GetShapeCount(); } }
				property Shape^ default[int] { 
					virtual Shape^ get(int index) { return Shape::FromNative(m_Parent->GetImpObj()->GetShape(index)); }
					virtual void set(int index, Shape^ value) { m_Parent->GetImpObj()->SetShape(index, value->ToNative()); }
				}
				property bool IsReadOnly { virtual bool get() { return false; } }
		
			protected:
				virtual bool Contains(Shape^ /*value*/) = System::Collections::Generic::ICollection<Shape^>::Contains { throw gcnew NotImplementedException(); }
				virtual void CopyTo(array<Shape^>^ /*arr*/, int /*index*/) = System::Collections::Generic::ICollection<Shape^>::CopyTo { throw gcnew NotImplementedException(); }
				virtual int IndexOf(Shape^ /*value*/) = System::Collections::Generic::IList<Shape^>::IndexOf { throw gcnew NotImplementedException(); }
				virtual bool Remove(Shape^ /*value*/) = System::Collections::Generic::IList<Shape^>::Remove { throw gcnew NotImplementedException(); }
				virtual System::Collections::IEnumerator^ GetEnumerator2() = System::Collections::IEnumerable::GetEnumerator { return this->GetEnumerator(); }
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
			property int Fields             { int get();     void set(int value); }
			property String^ Formula        { String^ get(); void set(String^ value); }
			property String^ FormulaBending { String^ get(); void set(String^ value); }
			property int Priority           { int get();     void set(int value); }

			property bool IsBuiltIn         { bool get(); }
			property bool IsUnknown         { bool get(); }
			property bool IsInternal        { bool get(); }

			void SetShapeTexts(String^ a, String^ b, String^ c, String^ d, String^ e, String^ f);
			void ClearShapeTexts(void);

			System::Drawing::Bitmap^ ToBitmap(Device^ device, View^ view, Model^ model, System::Drawing::Color backColor, int width, int height);
			System::Drawing::Bitmap^ ToBitmap(System::Drawing::Color backColor, int width, int height);

		public:
			static void AddPosShape(PosShape^ shape);
			static PosShape^ GetPosShape(String^ name);
			static PosShape^ GetUnknownPosShape();
			static bool HasPosShape(String^ name);
			static int GetPosShapeCount();
			static System::Collections::Generic::List<String^>^ GetAllPosShapes();
			static void ClearPosShapes();
			static void ReadPosShapesFromFile(String^ source);
			static void SavePosShapesToFile(String^ source);

		public:
			property ObjectId Id       { virtual ObjectId get() override; }
			property bool IsPersistent { virtual bool get() override; }

		protected:
			virtual int SubSetAttributes(Autodesk::AutoCAD::GraphicsInterface::DrawableTraits^ traits) override;
			virtual void SubViewportDraw(Autodesk::AutoCAD::GraphicsInterface::ViewportDraw^ vd) override;
			virtual int SubViewportDrawLogicalFlags(Autodesk::AutoCAD::GraphicsInterface::ViewportDraw^ vd) override;
			virtual bool SubWorldDraw(Autodesk::AutoCAD::GraphicsInterface::WorldDraw^ wd) override;
        };
    }

}