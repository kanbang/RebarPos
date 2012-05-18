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
				property Autodesk::AutoCAD::DatabaseServices::ObjectId ShapeId;

			public:
				BOQRow()
				{
					Pos = 0;
					Count = 0;
					Diameter = 0;
					Length1 = 0;
					Length2 = 0;
					IsVarLength = false;
					ShapeId = Autodesk::AutoCAD::DatabaseServices::ObjectId::Null;
				}
				BOQRow(int pos, int count, double diameter, double length1, double length2, bool isVarLength, Autodesk::AutoCAD::DatabaseServices::ObjectId shapeId)
				{
					Pos = pos;
					Count = count;
					Diameter = diameter;
					Length1 = length1;
					Length2 = length2;
					IsVarLength = isVarLength;
					ShapeId = shapeId;
				}

			private:
				BOQRow(BOQRow%) { }
				void operator=(BOQRow%) { }

			internal:
				CBOQRow* ToNative(void)
				{
					return new CBOQRow(Pos, Count, Diameter, Length1, Length2, IsVarLength ? Adesk::kTrue : Adesk::kFalse, OZOZ::RebarPosWrapper::Marshal::FromObjectId(ShapeId));
				}

			internal:
				static BOQRow^ FromNative(const CBOQRow* shape)
				{
					return gcnew BOQRow(shape->pos, shape->count, shape->diameter, shape->length1, shape->length2, (shape->isVarLength == Adesk::kTrue), OZOZ::RebarPosWrapper::Marshal::ToObjectId(shape->shapeId));
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
				void Add(int pos, int count, double diameter, double length1, double length2, bool isVarLength, Autodesk::AutoCAD::DatabaseServices::ObjectId shapeId);
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

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }

			property int Multiplier   { int get(); void set(int value); }

			property String^ Heading { String^ get(); void set(String^ value); }
			property String^ Footing { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId StyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
        };
    }
}