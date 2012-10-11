//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of BOQTable
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\BOQTable.h"
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
        public ref class BOQTable :  public Autodesk::AutoCAD::DatabaseServices::Entity
        {
		public:
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
            inline CBOQTable* GetImpObj()
            {
                return static_cast<CBOQTable*>(UnmanagedObject.ToPointer());
            }

        public:
			property BOQRowCollection^ Items { BOQRowCollection^ get(); }

			property Vector3d DirectionVector { Vector3d get(); }
			property Vector3d UpVector        { Vector3d get(); }
			property Vector3d NormalVector    { Vector3d get(); }

			property double Scale { double get(); void set(double value); }

			property double Width  { double get(); }
			property double Height { double get(); }

			property double MaxHeight    { double get(); void set(double value); }
			property double TableSpacing { double get(); void set(double value); }

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }

			property int Multiplier   { int get(); void set(int value); }

			property String^ Heading { String^ get(); void set(String^ value); }
			property String^ Footing { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId StyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

		public:
			void Update();
        };
    }
}