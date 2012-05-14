//-----------------------------------------------------------------------------
//----- Marshal.h : Marshalling functions
//-----------------------------------------------------------------------------

#pragma once

#include <msclr\marshal_cppstd.h>
#include <string>

namespace OZOZ
{
    namespace RebarPosWrapper 
    {
        public ref class Marshal
        {
        private:
			Marshal() { }
			Marshal(Marshal%) { }
			void operator=(Marshal%) { }

		public:
			// Convert System::String to const wchar_t*
			ref class StringToWchar
			{
			private:
				msclr::interop::marshal_context^ context;
				const wchar_t* m_ptr;
			    
			public:
				StringToWchar(System::String^ value)
				{
					context = gcnew msclr::interop::marshal_context();
					m_ptr = context->marshal_as<const wchar_t*>(value);
				}
				~StringToWchar()
				{
					delete context;
					m_ptr = 0;
				}
				operator const wchar_t*()
				{
					return m_ptr;
				}
			};

			// Convert const wchar_t* to System::String
			static inline System::String^ WcharToString(const wchar_t* value)
			{
				return msclr::interop::marshal_as<System::String^>(value);
			}

			// Convert System::String to const std::wstring
			static inline std::wstring StringToWstring(System::String^ value)
			{
				return std::wstring(StringToWchar(value));
			}

			// Convert const std::wstring to System::String
			static inline System::String^ WstringToString(const std::wstring& value)
			{
				return WcharToString(value.c_str());
			}

			// Convert const AcDbObjectId& to ObjectId
			static inline Autodesk::AutoCAD::DatabaseServices::ObjectId ToObjectId(const AcDbObjectId& value)
			{
				Autodesk::AutoCAD::DatabaseServices::ObjectId ret;
				AcDbObjectId* native = reinterpret_cast<AcDbObjectId*>(&(ret));
				*native = value;
				return ret;
			}

			// Convert ObjectId to const AcDbObjectId&
			static inline AcDbObjectId FromObjectId(Autodesk::AutoCAD::DatabaseServices::ObjectId value)
			{
				return *reinterpret_cast<AcDbObjectId*>(&(value));
			}

			// Convert const AcGeVector3d& to Vector3d
			static inline Autodesk::AutoCAD::Geometry::Vector3d ToVector3d(const AcGeVector3d& value)
			{
				Autodesk::AutoCAD::Geometry::Vector3d ret;
				AcGeVector3d* native = reinterpret_cast<AcGeVector3d*>(&(ret));
				*native = value;
				return ret;
			}

			// Convert Vector3d to const AcGeVector3d&
			static inline AcGeVector3d FromVector3d(Autodesk::AutoCAD::Geometry::Vector3d value)
			{
				return *reinterpret_cast<AcGeVector3d*>(&(value));
			}

			// Convert const AcGePoint3d& to Point3d
			static inline Autodesk::AutoCAD::Geometry::Point3d ToPoint3d(const AcGePoint3d& value)
			{
				Autodesk::AutoCAD::Geometry::Point3d ret;
				AcGePoint3d* native = reinterpret_cast<AcGePoint3d*>(&(ret));
				*native = value;
				return ret;
			}

			// Convert Point3d to const AcGePoint3d&
			static inline AcGePoint3d FromPoint3d(Autodesk::AutoCAD::Geometry::Point3d value)
			{
				return *reinterpret_cast<AcGePoint3d*>(&(value));
			}
		};
	}
}