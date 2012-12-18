//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of BOQTable
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\GenericTable.h"
#include "Marshal.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ 
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CGenericTable")]
        public ref class GenericTable :  public Autodesk::AutoCAD::DatabaseServices::Entity
        {
        public:
            GenericTable();

        internal:
            GenericTable(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CGenericTable* GetImpObj()
            {
                return static_cast<CGenericTable*>(UnmanagedObject.ToPointer());
            }

		public:
			virtual void SuspendUpdate();
			virtual void ResumeUpdate();

        public:
			property Vector3d DirectionVector { Vector3d get(); }
			property Vector3d UpVector        { Vector3d get(); }
			property Vector3d NormalVector    { Vector3d get(); }

			property double Scale { double get(); void set(double value); }

			property double Width  { double get(); }
			property double Height { double get(); }

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }
        };
    }
}