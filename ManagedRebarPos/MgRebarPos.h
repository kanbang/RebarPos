#pragma once

#include <tchar.h>
using namespace System;
using namespace Autodesk::AutoCAD::Geometry;
using namespace Autodesk::AutoCAD::DatabaseServices;

namespace OZOZ 
{
    namespace RebarPosWrapper 
    {
        [Autodesk::AutoCAD::Runtime::Wrapper("CRebarPos")]
        public ref class RebarPos :  public Autodesk::AutoCAD::DatabaseServices::Entity
        {

        public:
            RebarPos();

        internal:
            RebarPos(System::IntPtr unmanagedPointer, bool autoDelete);
            inline CRebarPos*  GetImpObj()
            {
                return static_cast<CRebarPos*>(UnmanagedObject.ToPointer());
            }

        public:
			property Point3d BasePoint { Point3d get(); void set(Point3d value); }
			property String^ Pos   { String^ get(); void set(String^ value); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId ShapeId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
        };
    }

}