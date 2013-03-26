//-----------------------------------------------------------------------------
//----- RebarPos.h : Declaration of RebarPos
//-----------------------------------------------------------------------------

#pragma once

#include "..\NativeRebarPos\RebarPos.h"
#include "MgPosGroup.h"
#include "MgPosShape.h"

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

			ref class CalculatedProperties
			{
			internal:
				int mGeneration;

			private:
				double mDiameter;
				int mCount;
				int mPrecision;
				int mFieldCount;
				bool mBending;
				int mMinCount, mMaxCount;
				bool mIsMultiCount;
				double mMinA, mMinB, mMinC, mMinD, mMinE, mMinF;
				double mMaxA, mMaxB, mMaxC, mMaxD, mMaxE, mMaxF;
				bool mIsVarA, mIsVarB, mIsVarC, mIsVarD, mIsVarE, mIsVarF;
				double mMinLength, mMaxLength;
				bool mIsVarLength;
				double mMinSpacing, mMaxSpacing;
				bool mIsVarSpacing;

			public:
				property double Diameter { double get() { return mDiameter; } }
				property int Count       { int    get() { return mCount; } }
				property int Precision   { int    get() { return mPrecision; } }
				property int FieldCount  { int    get() { return mFieldCount; } }
				property bool Bending    { bool   get() { return mBending; } }
				property int MinCount    { int    get() { return mMinCount; } }
				property int MaxCount    { int    get() { return mMaxCount; } }
				property bool IsMultiCount    { bool    get() { return mIsMultiCount; } }
				property double MinA     { double get() { return mMinA; } }
				property double MinB     { double get() { return mMinB; } }
				property double MinC     { double get() { return mMinC; } }
				property double MinD     { double get() { return mMinD; } }
				property double MinE     { double get() { return mMinE; } }
				property double MinF     { double get() { return mMinF; } }
				property double MaxA     { double get() { return mMaxA; } }
				property double MaxB     { double get() { return mMaxB; } }
				property double MaxC     { double get() { return mMaxC; } }
				property double MaxD     { double get() { return mMaxD; } }
				property double MaxE     { double get() { return mMaxE; } }
				property double MaxF     { double get() { return mMaxF; } }
				property bool IsVarA     { bool   get() { return mIsVarA; } }
				property bool IsVarB     { bool   get() { return mIsVarB; } }
				property bool IsVarC     { bool   get() { return mIsVarC; } }
				property bool IsVarD     { bool   get() { return mIsVarD; } }
				property bool IsVarE     { bool   get() { return mIsVarE; } }
				property bool IsVarF     { bool   get() { return mIsVarF; } }
				property double MinLength { double get() { return mMinLength; } }
				property double MaxLength { double get() { return mMaxLength; } }
				property bool IsVarLength { bool   get() { return mIsVarLength; } }
				property double MinSpacing { double get() { return mMinSpacing; } }
				property double MaxSpacing { double get() { return mMaxSpacing; } }
				property bool IsVarSpacing { bool   get() { return mIsVarSpacing; } }

			internal:
				CalculatedProperties()
				{
					mGeneration = -1;
				}

				CalculatedProperties(const CRebarPos::CCalculatedProperties& source)
				{
					mGeneration  = source.Generation;
					mCount       = source.Count;
					mDiameter    = source.Diameter;
					mPrecision	 = source.Precision;
					mFieldCount	 = source.FieldCount;
					mBending	 = source.Bending;
					mMinCount    = source.MinCount;
					mMaxCount    = source.MaxCount;
					mIsMultiCount = source.IsMultiCount;
					mMinA		 = source.MinA;
					mMinB		 = source.MinB;
					mMinC		 = source.MinC;
					mMinD		 = source.MinD;
					mMinE		 = source.MinE;
					mMinF		 = source.MinF;
					mMaxA		 = source.MaxA;
					mMaxB		 = source.MaxB;
					mMaxC		 = source.MaxC;
					mMaxD		 = source.MaxD;
					mMaxE		 = source.MaxE;
					mMaxF		 = source.MaxF;
					mIsVarA		 = source.IsVarA;
					mIsVarB		 = source.IsVarB;
					mIsVarC		 = source.IsVarC;
					mIsVarD		 = source.IsVarD;
					mIsVarE		 = source.IsVarE;
					mIsVarF		 = source.IsVarF;
					mMinLength	 = source.MinLength;
					mMaxLength	 = source.MaxLength;
					mIsVarLength = source.IsVarLength;
					mMinSpacing	 = source.MinSpacing;
					mMaxSpacing	 = source.MaxSpacing;
					mIsVarSpacing = source.IsVarSpacing;
				}
			};

		private:
			CalculatedProperties^ mCalculatedProperties;

        public:
			property Vector3d DirectionVector { Vector3d get(); }
			property Vector3d UpVector        { Vector3d get(); }
			property Vector3d NormalVector    { Vector3d get(); }

			property double Scale { double get(); void set(double value); }

			property double Width  { double get(); }
			property double Height { double get(); }

			property Point3d BasePoint { Point3d get(); void set(Point3d value); }
			property Point3d NoteGrip  { Point3d get(); void set(Point3d value); }
			property Point3d LengthGrip  { Point3d get(); void set(Point3d value); }

			property String^ Pos      { String^ get(); void set(String^ value); }
			property String^ Note     { String^ get(); void set(String^ value); }
			property String^ Count    { String^ get(); void set(String^ value); }
			property String^ Diameter { String^ get(); void set(String^ value); }
			property String^ Spacing  { String^ get(); void set(String^ value); }
			property bool IncludeInBOQ { bool get(); void set(bool value); }
			property int Multiplier   { int get(); void set(int value); }

			property DisplayStyle Display { DisplayStyle get(); void set(DisplayStyle value); }

			property String^ Shape { String^ get(); void set(String^ value); }
			property String^ A { String^ get(); void set(String^ value); }
			property String^ B { String^ get(); void set(String^ value); }
			property String^ C { String^ get(); void set(String^ value); }
			property String^ D { String^ get(); void set(String^ value); }
			property String^ E { String^ get(); void set(String^ value); }
			property String^ F { String^ get(); void set(String^ value); }

			property bool Detached { bool get(); void set(bool value); }

			property CalculatedProperties^ CalcProperties { CalculatedProperties^ get(); }

			property String^ Length { String^ get(); }

			property String^ PosKey { String^ get(); }

			property array<PosShape::Shape^>^ Shapes { array<PosShape::Shape^>^ get(); }

			property Autodesk::AutoCAD::DatabaseServices::ObjectId GroupIdForDisplay { Autodesk::AutoCAD::DatabaseServices::ObjectId get(); void set(Autodesk::AutoCAD::DatabaseServices::ObjectId value); }

		public:
			void Update();
			HitTestResult HitTest(Point3d pt);
			void TextBox([Out] Point3d% minPoint, [Out] Point3d% maxPoint);

		public:
			static bool GetTotalLengths(String^ formula, int fieldCount, PosGroup::DrawingUnits inputUnit, String^ a, String^ b, String^ c, String^ d, String^ e, String^ f, String^ diameter, [Out] double% minLength, [Out] double% maxLength, [Out] bool% isVar);
			static double ConvertLength(double length, PosGroup::DrawingUnits fromUnit, PosGroup::DrawingUnits toUnit);
			static double BendingRadius(double d);
        };
    }
}