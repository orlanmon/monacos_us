﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiteDAO
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="monacos.us")]
	public partial class Monacos_usDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContentArea(ContentArea instance);
    partial void UpdateContentArea(ContentArea instance);
    partial void DeleteContentArea(ContentArea instance);
    partial void InsertContent(Content instance);
    partial void UpdateContent(Content instance);
    partial void DeleteContent(Content instance);
    partial void InsertGPS_Tracking(GPS_Tracking instance);
    partial void UpdateGPS_Tracking(GPS_Tracking instance);
    partial void DeleteGPS_Tracking(GPS_Tracking instance);
    #endregion
		
		public Monacos_usDataContext() : 
				base(global::SiteDAO.Properties.Settings.Default.monacos_usConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public Monacos_usDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Monacos_usDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Monacos_usDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Monacos_usDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ContentArea> ContentAreas
		{
			get
			{
				return this.GetTable<ContentArea>();
			}
		}
		
		public System.Data.Linq.Table<Content> Contents
		{
			get
			{
				return this.GetTable<Content>();
			}
		}
		
		public System.Data.Linq.Table<GPS_Tracking> GPS_Trackings
		{
			get
			{
				return this.GetTable<GPS_Tracking>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentArea")]
	public partial class ContentArea : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ContentArea_ID;
		
		private string _ContentArea_Description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnContentArea_IDChanging(int value);
    partial void OnContentArea_IDChanged();
    partial void OnContentArea_DescriptionChanging(string value);
    partial void OnContentArea_DescriptionChanged();
    #endregion
		
		public ContentArea()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentArea_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ContentArea_ID
		{
			get
			{
				return this._ContentArea_ID;
			}
			set
			{
				if ((this._ContentArea_ID != value))
				{
					this.OnContentArea_IDChanging(value);
					this.SendPropertyChanging();
					this._ContentArea_ID = value;
					this.SendPropertyChanged("ContentArea_ID");
					this.OnContentArea_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentArea_Description", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string ContentArea_Description
		{
			get
			{
				return this._ContentArea_Description;
			}
			set
			{
				if ((this._ContentArea_Description != value))
				{
					this.OnContentArea_DescriptionChanging(value);
					this.SendPropertyChanging();
					this._ContentArea_Description = value;
					this.SendPropertyChanged("ContentArea_Description");
					this.OnContentArea_DescriptionChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Content")]
	public partial class Content : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Content_ID;
		
		private int _ContentArea_ID;
		
		private string _Description;
		
		private string _ContentValue;
		
		private System.DateTime _Create_Date;
		
		private System.DateTime _Publish_Date;
		
		private System.Nullable<System.DateTime> _Expiration_Date;
		
		private bool _Active;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnContent_IDChanging(int value);
    partial void OnContent_IDChanged();
    partial void OnContentArea_IDChanging(int value);
    partial void OnContentArea_IDChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnContentValueChanging(string value);
    partial void OnContentValueChanged();
    partial void OnCreate_DateChanging(System.DateTime value);
    partial void OnCreate_DateChanged();
    partial void OnPublish_DateChanging(System.DateTime value);
    partial void OnPublish_DateChanged();
    partial void OnExpiration_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnExpiration_DateChanged();
    partial void OnActiveChanging(bool value);
    partial void OnActiveChanged();
    #endregion
		
		public Content()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Content_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Content_ID
		{
			get
			{
				return this._Content_ID;
			}
			set
			{
				if ((this._Content_ID != value))
				{
					this.OnContent_IDChanging(value);
					this.SendPropertyChanging();
					this._Content_ID = value;
					this.SendPropertyChanged("Content_ID");
					this.OnContent_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentArea_ID", DbType="Int NOT NULL")]
		public int ContentArea_ID
		{
			get
			{
				return this._ContentArea_ID;
			}
			set
			{
				if ((this._ContentArea_ID != value))
				{
					this.OnContentArea_IDChanging(value);
					this.SendPropertyChanging();
					this._ContentArea_ID = value;
					this.SendPropertyChanged("ContentArea_ID");
					this.OnContentArea_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentValue", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string ContentValue
		{
			get
			{
				return this._ContentValue;
			}
			set
			{
				if ((this._ContentValue != value))
				{
					this.OnContentValueChanging(value);
					this.SendPropertyChanging();
					this._ContentValue = value;
					this.SendPropertyChanged("ContentValue");
					this.OnContentValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Create_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Create_Date
		{
			get
			{
				return this._Create_Date;
			}
			set
			{
				if ((this._Create_Date != value))
				{
					this.OnCreate_DateChanging(value);
					this.SendPropertyChanging();
					this._Create_Date = value;
					this.SendPropertyChanged("Create_Date");
					this.OnCreate_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Publish_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Publish_Date
		{
			get
			{
				return this._Publish_Date;
			}
			set
			{
				if ((this._Publish_Date != value))
				{
					this.OnPublish_DateChanging(value);
					this.SendPropertyChanging();
					this._Publish_Date = value;
					this.SendPropertyChanged("Publish_Date");
					this.OnPublish_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Expiration_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Expiration_Date
		{
			get
			{
				return this._Expiration_Date;
			}
			set
			{
				if ((this._Expiration_Date != value))
				{
					this.OnExpiration_DateChanging(value);
					this.SendPropertyChanging();
					this._Expiration_Date = value;
					this.SendPropertyChanged("Expiration_Date");
					this.OnExpiration_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Active", DbType="Bit NOT NULL")]
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				if ((this._Active != value))
				{
					this.OnActiveChanging(value);
					this.SendPropertyChanging();
					this._Active = value;
					this.SendPropertyChanged("Active");
					this.OnActiveChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GPS_Tracking")]
	public partial class GPS_Tracking : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GPS_TrackingID;
		
		private string _Device_ID;
		
		private string _Latitude;
		
		private string _Longitude;
		
		private System.DateTime _Entry_Date;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGPS_TrackingIDChanging(int value);
    partial void OnGPS_TrackingIDChanged();
    partial void OnDevice_IDChanging(string value);
    partial void OnDevice_IDChanged();
    partial void OnLatitudeChanging(string value);
    partial void OnLatitudeChanged();
    partial void OnLongitudeChanging(string value);
    partial void OnLongitudeChanged();
    partial void OnEntry_DateChanging(System.DateTime value);
    partial void OnEntry_DateChanged();
    #endregion
		
		public GPS_Tracking()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GPS_TrackingID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GPS_TrackingID
		{
			get
			{
				return this._GPS_TrackingID;
			}
			set
			{
				if ((this._GPS_TrackingID != value))
				{
					this.OnGPS_TrackingIDChanging(value);
					this.SendPropertyChanging();
					this._GPS_TrackingID = value;
					this.SendPropertyChanged("GPS_TrackingID");
					this.OnGPS_TrackingIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Device_ID", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Device_ID
		{
			get
			{
				return this._Device_ID;
			}
			set
			{
				if ((this._Device_ID != value))
				{
					this.OnDevice_IDChanging(value);
					this.SendPropertyChanging();
					this._Device_ID = value;
					this.SendPropertyChanged("Device_ID");
					this.OnDevice_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Latitude", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this.OnLatitudeChanging(value);
					this.SendPropertyChanging();
					this._Latitude = value;
					this.SendPropertyChanged("Latitude");
					this.OnLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Longitude", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this.OnLongitudeChanging(value);
					this.SendPropertyChanging();
					this._Longitude = value;
					this.SendPropertyChanged("Longitude");
					this.OnLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Entry_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Entry_Date
		{
			get
			{
				return this._Entry_Date;
			}
			set
			{
				if ((this._Entry_Date != value))
				{
					this.OnEntry_DateChanging(value);
					this.SendPropertyChanging();
					this._Entry_Date = value;
					this.SendPropertyChanged("Entry_Date");
					this.OnEntry_DateChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
