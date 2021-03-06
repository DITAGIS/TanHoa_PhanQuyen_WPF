﻿using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class HoaDonDBViewModel
    {
        private const String TABLE_NAME_BAOTHAY = "BaoThay";
        private const String TABLE_NAME_DOCSO = "Docso";
        private const String TABLE_NAME_KHACHHANG = "KhachHang";
        private const String TABLE_NAME_HINHDHN = "HinhDHN";
        private const String TABLE_NAME_TO = "[DocSoTH].[dbo].[To]";
        private const String SQL_SELECT = "select top 100 * from " + TABLE_NAME_DOCSO;
        private const String SQL_SELECT_DANH_BA_CONDITION = "select top 15 docso.danhba  from " +
        TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + ", " + TABLE_NAME_HINHDHN + " where nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa " +
        "and docso.DanhBa = HinhDHN.DanhBo and docso.GIOGHI = HinhDHN.CreateDate";
        private const String SQL_SELECT_INCLUDE_IMAGE_CONDITION = "select TTDHNCu, TTDHNMoi, CodeMoi, CodeCu, CSCu, CSMOI, Tieuthumoi, TBTT, ghichuds," +
            " KhachHang.So, KhachHang.Duong, KhachHang.TenKH, KhachHang.GB, KhachHang.DM, KhachHang.HopDong, KhachHang.Hieu, KhachHang.Co, KhachHang.SoThan, KhachHang.MLT1, [Image]  from " +
            TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + ", " + TABLE_NAME_HINHDHN + " where docso.danhba = @danhba and nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa " +
            "and docso.DanhBa = HinhDHN.DanhBo and docso.GIOGHI = HinhDHN.CreateDate";
        private const String SQL_SELECT_1_MONTH = "select gioghi, codemoi, csmoi, Tieuthumoi from " +
           TABLE_NAME_DOCSO + " where docso.danhba = @danhba and nam = @year and ky = @month ";
        private const String SQL_SELECT_BAOTHAY = "select loaiBT, csgo, csgan, sothanmoi, ngaythay, ngaycapnhat from " + TABLE_NAME_BAOTHAY + " where danhba = @danhba";
        private const String SQL_SELECT_CONDITION = "select docso.danhba, TTDHNCu, TTDHNMoi, CodeMoi, CodeCu, CSCu, CSMOI, Tieuthumoi, TBTT, ghichuds," +
         " KhachHang.So, KhachHang.Duong, KhachHang.TenKH, KhachHang.GB, KhachHang.DM, KhachHang.HopDong, KhachHang.Hieu, KhachHang.Co, KhachHang.SoThan, KhachHang.MLT1  from " +
         TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + " where nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa";
        private const String SQL_SELECT_DISTINCT_YEAR = "select distinct nam from " + TABLE_NAME_DOCSO;
        private const String SQL_SELECT_DISTINCT_MONTH = "select distinct ky from " + TABLE_NAME_DOCSO + " where nam = @year";
        private const String SQL_SELECT_DISTINCT_DATE = "select distinct dot from " + TABLE_NAME_DOCSO + " where nam = @year and ky = @month";
        private const String SQL_SELECT_DISTINCT_GROUP = "select distinct mato from " + TABLE_NAME_TO;
        private const String SQL_SELECT_DISTINCT_MACHINE = "select [TuMay], [DenMay] from " + TABLE_NAME_TO + " where mato = @group";
        public static int MAX = 1;
        public static int VALUE = 0;
        private static HoaDonDBViewModel _instance;
        //private static DataClassServerDataContext _serverDataContext;
        public static HoaDonDBViewModel getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HoaDonDBViewModel();
                    //_serverDataContext = new DataClassServerDataContext();
                }
                return _instance;
            }
        }
        private HoaDonDBViewModel() { }

        public List<HoaDon> getHoaDonsByCondition(String year, String month, String date, String group, String machine)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            //try
            //{
            //    SqlCommand command = new SqlCommand(SQL_SELECT_CONDITION, ConnectionViewModel.getInstance.getConnection);
            //    //ConnectionViewModel.getInstance.Connect();
            //    command.Parameters.AddWithValue("@year", year);
            //    command.Parameters.AddWithValue("@month", month);
            //    command.Parameters.AddWithValue("@date", date);
            //    //command.Parameters.AddWithValue("@group", group);
            //    command.Parameters.AddWithValue("@machine", machine);
            //    SqlDataReader dataReader = command.ExecuteReader();
            //    while (dataReader.Read())
            //    {
            //        HoaDon hoaDon = new HoaDon();
            //        hoaDon.DanhBa = dataReader["danhba"].ToString();
            //        hoaDon.TTHDNCu = dataReader["TTDHNCU"].ToString();
            //        hoaDon.TTHDNMoi = dataReader["TTDHNMoi"].ToString();
            //        hoaDon.CodeMoi = dataReader["CodeMoi"].ToString();
            //        hoaDon.CodeCu = dataReader["CodeCu"].ToString();
            //        hoaDon.CSC = dataReader["CSCU"].ToString();
            //        hoaDon.CSM = dataReader["CSMOI"].ToString();
            //        hoaDon.TieuThuMoi = dataReader["TieuThuMoi"].ToString();
            //        hoaDon.TBTT = dataReader["TBTT"].ToString();
            //        hoaDon.DiaChi = dataReader["so"].ToString() + " " + dataReader["duong"].ToString(); ;
            //        hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
            //        hoaDon.TenKH = dataReader["TenKH"].ToString();
            //        hoaDon.HopDong = dataReader["HopDong"].ToString();
            //        hoaDon.Hieu = dataReader["Hieu"].ToString();
            //        hoaDon.Co = dataReader["Co"].ToString();
            //        hoaDon.GB = dataReader["GB"].ToString();
            //        hoaDon.DM = dataReader["DM"].ToString();
            //        hoaDon.SoThan = dataReader["SoThan"].ToString();
            //        hoaDon.MLT = dataReader["MLT1"].ToString();
            //        hoaDons.Add(hoaDon);
            //    }
            //}
            //catch (Exception e)
            //{

            //}
            return hoaDons;
        }
        public HoaDon getHoaDonsIncludeImageByCondition(String year, String month, String date, String group, String machine, String danhba)
        {
            HoaDon hoaDon = new HoaDon(); ;
            //SqlDataReader dataReader = null;
            //SqlDataReader dataReader1 = null;
            //try
            //{
            //    SqlCommand command = new SqlCommand(SQL_SELECT_INCLUDE_IMAGE_CONDITION, ConnectionViewModel.getInstance.getConnection);
            //    //ConnectionViewModel.getInstance.Connect();
            //    command.Parameters.AddWithValue("@danhba", danhba);
            //    command.Parameters.AddWithValue("@year", year);
            //    command.Parameters.AddWithValue("@month", month);
            //    command.Parameters.AddWithValue("@date", date);
            //    //command.Parameters.AddWithValue("@group", group);
            //    command.Parameters.AddWithValue("@machine", machine);
            //    dataReader = command.ExecuteReader();
            //    while (dataReader.Read())
            //    {
            //        hoaDon.DanhBa = danhba;
            //        hoaDon.TTHDNCu = dataReader["TTDHNCU"].ToString();
            //        hoaDon.TTHDNMoi = dataReader["TTDHNMoi"].ToString();
            //        hoaDon.CodeMoi = dataReader["CodeMoi"].ToString();
            //        hoaDon.CodeCu = dataReader["CodeCu"].ToString();
            //        hoaDon.CSC = dataReader["CSCU"].ToString();
            //        hoaDon.CSM = dataReader["CSMOI"].ToString();
            //        hoaDon.TieuThuMoi = dataReader["TieuThuMoi"].ToString();
            //        hoaDon.TBTT = dataReader["TBTT"].ToString();
            //        hoaDon.DiaChi = dataReader["so"].ToString() + " " + dataReader["duong"].ToString(); ;
            //        hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
            //        hoaDon.TenKH = dataReader["TenKH"].ToString();
            //        hoaDon.HopDong = dataReader["HopDong"].ToString();
            //        hoaDon.Hieu = dataReader["Hieu"].ToString();
            //        hoaDon.Co = dataReader["Co"].ToString();
            //        hoaDon.GB = dataReader["GB"].ToString();
            //        hoaDon.DM = dataReader["DM"].ToString();
            //        hoaDon.SoThan = dataReader["SoThan"].ToString();
            //        hoaDon.MLT = dataReader["MLT1"].ToString();
            //        hoaDon.Image = dataReader["Image"] as Byte[];
            //    }
            //    if (!dataReader.IsClosed)
            //        dataReader.Close();

            //    command = new SqlCommand(SQL_SELECT_BAOTHAY, ConnectionViewModel.getInstance.getConnection);
            //    command.Parameters.AddWithValue("@danhba", danhba);
            //    dataReader1 = command.ExecuteReader();
            //    while (dataReader1.Read())
            //    {
            //        hoaDon.LoaiBaoThay = dataReader1["loaibt"].ToString();
            //        hoaDon.ChiSoGo = dataReader1["csgo"].ToString();
            //        hoaDon.ChiSoGan = dataReader1["csgan"].ToString();
            //        hoaDon.SoThanMoi = dataReader1["sothanmoi"].ToString();
            //        hoaDon.NgayThay = dataReader1["ngaythay"].ToString();
            //        hoaDon.NgayCapNhat = dataReader1["ngaycapnhat"].ToString();
            //        if (hoaDon.NgayCapNhat == null)
            //            hoaDon.NgayCapNhat = "";
            //    }
            //}
            //catch (Exception e)
            //{

            //}

            //if (dataReader1 != null && !dataReader1.IsClosed)
            //    dataReader1.Close();
            return hoaDon;
        }
        public HoaDon getHoaDons1MonthByCondition(String year, String month, String danhba)
        {
            HoaDon hoaDon = new HoaDon(); ;
            //SqlDataReader dataReader = null;

            //try
            //{
            //    SqlCommand command = new SqlCommand(SQL_SELECT_1_MONTH, ConnectionViewModel.getInstance.getConnection);
            //    //ConnectionViewModel.getInstance.Connect();
            //    command.Parameters.AddWithValue("@danhba", danhba);
            //    command.Parameters.AddWithValue("@year", year);
            //    command.Parameters.AddWithValue("@month", month);

            //    dataReader = command.ExecuteReader();
            //    while (dataReader.Read())
            //    {
            //        hoaDon.DanhBa = danhba;

            //        hoaDon.CodeMoi = dataReader["CodeMoi"].ToString();
            //        hoaDon.GioGhi = dataReader["gioghi"].ToString();
            //        hoaDon.CSM = dataReader["CSMOI"].ToString();
            //        hoaDon.TieuThuMoi = dataReader["TieuThuMoi"].ToString();
            //    }
            //}
            //catch (Exception e)
            //{

            //}

            //if (dataReader != null && !dataReader.IsClosed)
            //    dataReader.Close();
            return hoaDon;
        }
        public List<String> getDanhBasByCondition(int year, String month, String date, int xGroup, String machine)
        {
            List<String> danhBas = new List<String>();
            SqlCommand command = new SqlCommand(SQL_SELECT_DANH_BA_CONDITION, ConnectionViewModel.Instance.getConnection);
            SqlDataReader dataReader = null;
            try
            {
                DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
                var getData = (from x in localContext.DocSoLocals
                               where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine
                               select x.DanhBa).ToList();
                danhBas.AddRange(getData);
                ////ConnectionViewModel.getInstance.Connect();
                //command.Parameters.AddWithValue("@year", year);
                //command.Parameters.AddWithValue("@month", month);
                //command.Parameters.AddWithValue("@date", date);
                ////command.Parameters.AddWithValue("@group", group);
                //command.Parameters.AddWithValue("@machine", machine);
                //dataReader = command.ExecuteReader();
                //while (dataReader.Read())
                //{
                //    danhBas.Add(dataReader["danhba"].ToString());
                //}
            }
            catch 
            {

            }
            if (dataReader != null && !dataReader.IsClosed)
                dataReader.Close();
            return danhBas;
        }
        public List<String> getDistinctYear()
        {
            List<String> listYear = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_YEAR, ConnectionViewModel.Instance.getConnection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listYear.Add(dataReader["nam"].ToString());
                dataReader.Close();
            }
            catch 
            {

            }
            return listYear;
        }
        public List<String> getDistinctMonth(String year)
        {
            List<String> listMonth = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_MONTH, ConnectionViewModel.Instance.getConnection);
                command.Parameters.AddWithValue("@year", year);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listMonth.Add(dataReader["ky"].ToString());
                dataReader.Close();
            }
            catch 
            {

            }
            return listMonth;
        }
        public List<String> getDistinctDate(String year, String month)
        {
            List<String> listDate = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_DATE, ConnectionViewModel.Instance.getConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listDate.Add(dataReader["dot"].ToString());
                dataReader.Close();
            }
            catch 
            {

            }
            return listDate;
        }
        public List<String> getDistinctGroup()
        {
            List<String> listGroup = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_GROUP, ConnectionViewModel.Instance.getConnection);

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listGroup.Add(dataReader["mato"].ToString());
                dataReader.Close();
            }
            catch 
            {

            }
            return listGroup;
        }
        public List<String> getDistinctMachine(String group)
        {
            List<String> listMachine = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_MACHINE, ConnectionViewModel.Instance.getConnection);

                command.Parameters.AddWithValue("@group", group);
                SqlDataReader dataReader = command.ExecuteReader();
                int start = 0, end = 0;
                while (dataReader.Read())
                {
                    start = Int16.Parse(dataReader["tumay"].ToString());
                    end = Int16.Parse(dataReader["denmay"].ToString());
                }
                dataReader.Close();
                for (int i = start; i <= end; i++)
                    if (i < 10)
                        listMachine.Add("0" + i);
                    else
                        listMachine.Add(i.ToString());
            }
            catch 
            {

            }
            return listMachine;
        }
    }
}
