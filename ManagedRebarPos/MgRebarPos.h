//-----------------------------------------------------------------------------
//----- RebarPos.h : Declaration of RebarPos
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\RebarPos.h"

using namespace System;
using namespace System::Runtime::InteropServices;
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
            inline CRebarPos* GetImpObj()
            {
                return static_cast<CRebarPos*>(UnmanagedObject.ToPointer());
            }

		public:
			enum class HitTestResult
			{ 
				None = 0,
				Pos = 1,
				Count = 2,
				Diameter = 3,
				Spacing = 4,
				Group = 5,
				Multiplier = 6,
				Length = 7,
				Note = 8
			};
			enum class DisplayStyle
			{ 
				All = 0,
				WithoutLength = 1,
				MarkerOnly = 2,
			};

        public:
			property Vector3d DirectionVector { Vector3d get(); }
			property Vector3d UpVector        { Vector3d get(); }
			property Vector3d NormalVector    { Vector3d get(); }

			property double Width  { double get(); }
			property double Height { double get(); }

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }
			property Point3d NoteGrip  { Point3d get(); void set(Point3d value); }

			property String^ Pos      { String^ get(); void set(String^ value); }
			property String^ Note     { String^ get(); void set(String^ value); }
			property String^ Count    { String^ get(); void set(String^ value); }
			property String^ Diameter { String^ get(); void set(String^ value); }
			property String^ Spacing  { String^ get(); void set(String^ value); }
			property int Multiplier   { int get(); void set(int value); }

			property DisplayStyle Display { DisplayStyle get(); void set(DisplayStyle value); }

			property String^ A { String^ get(); void set(String^ value); }
			property String^ B { String^ get(); void set(String^ value); }
			property String^ C { String^ get(); void set(String^ value); }
			property String^ D { String^ get(); void set(String^ value); }
			property String^ E { String^ get(); void set(String^ value); }
			property String^ F { String^ get(); void set(String^ value); }

			property bool IsVarLength { bool get(); }
			property String^ Length { String^ get(); }
			property double MinLength { double get(); }
			property double MaxLength { double get(); }

			property String^ PosKey { String^ get(); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId ShapeId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }
			property Autodesk::AutoCAD::DatabaseServices::ObjectId GroupId { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

		public:
			void Update();
			HitTestResult HitTest(Point3d pt);

		public:
			static bool GetTotalLengths(String^ formula, int fieldCount, double scale, String^ a, String^ b, String^ c, String^ d, String^ e, String^ f, String^ diameter, int precision, [Out] double% minLength, [Out] double% maxLength, [Out] bool% isVar);
			static double BendingRadius(double d);
        };
    }

}