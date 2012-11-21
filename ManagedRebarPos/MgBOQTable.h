//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of BOQTable
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\BOQTable.h"
#include "MgGenericTable.h"
#include "Marshal.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ 
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CBOQTable")]
        public ref class BOQTable :  public GenericTable
        {
		public:
			enum class DrawingUnits
			{ 
				Millimeter = 0,
				Centimeter = 1,
				Decimeter = 2,
				Meter = 3,
			};

			ref struct BOQRow
			{
			public:
				property int Pos;
				property int Count;
				property double Diameter;
				property double Length1;
				property double Length2;
				property bool IsVarLength;
				property bool IsEmpty;
				property System::String^ Shape;
				property System::String^ A;
				property System::String^ B;
				property System::String^ C;
				property System::String^ D;
				property System::String^ E;
				property System::String^ F;

			public:
				BOQRow()
				{
					Pos = 0;
					Count = 0;
					Diameter = 0;
					Length1 = 0;
					Length2 = 0;
					IsVarLength = false;
					IsEmpty = false;
				}
				BOQRow(int pos, int count, double diameter, double length1, double length2, bool isVarLength, System::String^ shape, System::String^ a, System::String^ b, System::String^ c, System::String^ d, System::String^ e, System::String^ f)
				{
					Pos = pos;
					Count = count;
					Diameter = diameter;
					Length1 = length1;
					Length2 = length2;
					IsVarLength = isVarLength;
					IsEmpty = false;
					Shape = shape;
					A = a;
					B = b;
					C = c;
					D = d;
					E = e;
					F = f;
				}
				BOQRow(int pos)
				{
					Pos = pos;
					Count = 0;
					Diameter = 0;
					Length1 = 0;
					Length2 = 0;
					IsVarLength = false;
					IsEmpty = true;
				}

			private:
				BOQRow(BOQRow%) { }
				void operator=(BOQRow%) { }

			internal:
				CBOQRow* ToNative(void)
				{
					if(!IsEmpty)
						return new CBOQRow(Pos, Count, Diameter, Length1, Length2, IsVarLength ? Adesk::kTrue : Adesk::kFalse, OZOZ::RebarPosWrapper::Marshal::StringToWchar(Shape), 
						OZOZ::RebarPosWrapper::Marshal::StringToWchar(A), OZOZ::RebarPosWrapper::Marshal::StringToWchar(B), OZOZ::RebarPosWrapper::Marshal::StringToWchar(C), OZOZ::RebarPosWrapper::Marshal::StringToWchar(D), OZOZ::RebarPosWrapper::Marshal::StringToWchar(E), OZOZ::RebarPosWrapper::Marshal::StringToWchar(F));
					else
						return new CBOQRow(Pos);
				}

			internal:
				static BOQRow^ FromNative(const CBOQRow* shape)
				{
					if(!shape->isEmpty)
						return gcnew BOQRow(shape->pos, shape->count, shape->diameter, shape->length1, shape->length2, (shape->isVarLength == Adesk::kTrue), Marshal::WcharToString(shape->shape.c_str()),
						Marshal::WcharToString(shape->a.c_str()), Marshal::WcharToString(shape->b.c_str()), Marshal::WcharToString(shape->c.c_str()), Marshal::WcharToString(shape->d.c_str()), Marshal::WcharToString(shape->e.c_str()), Marshal::WcharToString(shape->f.c_str()));
					else
						return gcnew BOQRow(shape->pos);
				}
			};
			
		public:
			ref class BOQRowCollection
			{
			private:
				BOQTable^ m_Parent;

			private:
				BOQRowCollection() { }
				BOQRowCollection(BOQRowCollection%) { }
				void operator=(BOQRowCollection%) { }

			internal:
				BOQRowCollection(BOQTable^ parent);

			public:
				void Add(int pos, int count, double diameter, double length1, double length2, bool isVarLength, System::String^ shape, System::String^ a, System::String^ b, System::String^ c, System::String^ d, System::String^ e, System::String^ f);
				void Add(int pos, int count, double diameter, double length, System::String^ shape, System::String^ a, System::String^ b, System::String^ c, System::String^ d, System::String^ e, System::String^ f);
				void Add(int pos);
				property int Count { int get(); }
				property BOQRow^ default[int] { BOQRow^ get(int index); void set(int index, BOQRow^ value); }
				void Remove(int index);
				void Clear();
			};

		protected:
			BOQRowCollection^ m_Rows;

        public:
            BOQTable();

        internal:
            BOQTable(System::IntPtr unmanagedPointer, bool autoDelete);
            CBOQTable* GetImpObj() new
            {
                return static_cast<CBOQTable*>(UnmanagedObject.ToPointer());
            }

        public:
			property BOQRowCollection^ Items { BOQRowCollection^ get(); }

			property int Multiplier   { int get(); void set(int value); }

			property String^ Heading { String^ get(); void set(String^ value); }
			property String^ Footing { String^ get(); void set(String^ value); }
			property String^ Note    { String^ get(); void set(String^ value); }

			property int Precision       { int get(); void set(int value); }
			property DrawingUnits DisplayUnit { DrawingUnits get(); void set(DrawingUnits value); }

			property String^ ColumnDef           { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::Colors::Color^ TextColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ PosColor        { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ LineColor       { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ SeparatorColor  { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ BorderColor     { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ HeadingColor    { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }
			property Autodesk::AutoCAD::Colors::Color^ FootingColor    { Autodesk::AutoCAD::Colors::Color^ get(); void set(Autodesk::AutoCAD::Colors::Color^ value); }

			property String^ PosLabel            { String^ get(); void set(String^ value); }
			property String^ CountLabel          { String^ get(); void set(String^ value); }
			property String^ DiameterLabel       { String^ get(); void set(String^ value); }
			property String^ LengthLabel         { String^ get(); void set(String^ value); }
			property String^ ShapeLabel          { String^ get(); void set(String^ value); }
			property String^ TotalLengthLabel    { String^ get(); void set(String^ value); }
			property String^ DiameterListLabel   { String^ get(); void set(String^ value); }
			property String^ DiameterLengthLabel { String^ get(); void set(String^ value); }
			property String^ UnitWeightLabel     { String^ get(); void set(String^ value); }
			property String^ WeightLabel         { String^ get(); void set(String^ value); }
			property String^ GrossWeightLabel    { String^ get(); void set(String^ value); }
			property String^ MultiplierHeadingLabel { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId TextStyleId    { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId HeadingStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId FootingStyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property double HeadingScale { double get(); void set(double value); }
			property double FootingScale { double get(); void set(double value); }
			property double RowSpacing   { double get(); void set(double value); }

		public:
			void Update();
        };
    }
}