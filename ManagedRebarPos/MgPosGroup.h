//-----------------------------------------------------------------------------
//----- PosGroup.h : Declaration of PosGroup
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\PosGroup.h"

using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CPosGroup")]
        public ref class PosGroup :  public Autodesk::AutoCAD::DatabaseServices::DBObject
        {
        public:
            PosGroup();

        internal:
            PosGroup(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CPosGroup* GetImpObj()
            {
                return static_cast<CPosGroup*>(UnmanagedObject.ToPointer());
            }
		public:
			enum class DrawingUnits
			{ 
				Millimeter = 0,
				Centimeter = 1,
				Decimeter = 2,
				Meter = 3,
			};

		public:
			property String^ Name        { String^ get(); void set(String^ value); }
			property bool Bending        { bool get(); void set(bool value); }
			property double MaxBarLength { double get(); void set(double value); }
			property int Precision       { int get(); void set(int value); }

			property PosGroup::DrawingUnits DrawingUnit { PosGroup::DrawingUnits get(); void set(PosGroup::DrawingUnits value); }
			property PosGroup::DrawingUnits DisplayUnit { PosGroup::DrawingUnits get(); void set(PosGroup::DrawingUnits value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId StyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

			property bool Current { bool get(); void set(bool value); }

		public:
			property static String^ TableName        { String^ get(); }
		};
	}
}