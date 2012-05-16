//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of BOQTable
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\BOQTable.h"

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
            BOQTable();

        internal:
            BOQTable(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CBOQTable* GetImpObj()
            {
                return static_cast<CBOQTable*>(UnmanagedObject.ToPointer());
            }

        public:
			property Vector3d DirectionVector { Vector3d get(); }
			property Vector3d UpVector        { Vector3d get(); }
			property Vector3d NormalVector    { Vector3d get(); }

			property double Scale { double get(); void set(double value); }

			property double Width  { double get(); }
			property double Height { double get(); }

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }

			property int Multiplier   { int get(); void set(int value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId StyleId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
        };
    }
}