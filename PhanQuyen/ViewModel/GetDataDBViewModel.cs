﻿using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ViewModel
{
    public class DataDBViewModel
    {


        private static DataDBViewModel _instance;
        private DataClassesLocalDataContext localContext;
        private DataClassServerDataContext serverContext;
        private const String TTKH_COLUMN_TENKH = "TenKH";
        private const String TTKH_COLUMN_HIEU = "Hieu";
        private const String TTKH_COLUMN_CO = "Co";
        private const String TTKH_COLUMN_SDT = "SDT";
        private const String TTKH_COLUMN_DUONG = "Duong";
        private const String TTKH_COLUMN_SOTHAN = "SoThan";
        private const String TTKH_COLUMN_MLT1 = "MLT1";
        private const String TTKH_COLUMN_HOPDONG = "HopDong";
        private const String TTKH_COLUMN_DANHBA = "DanhBa";
        private const String TTKH_COLUMN_GB = "GB";
        private const String TTKH_COLUMN_DM = "DM";
        private const String TTKH_COLUMN_KY = "KY";//todo
        private const String TTKH_COLUMN_NGAYDOC = "DenNgay";
        private const String TTKH_COLUMN_CODE = "Code";
        private const String TTKH_COLUMN_CSMOI = "CSMoi";
        private const String TTKH_COLUMN_TIEUTHU = "TieuThu";
        private const String TTKH_COLUMN_GHICHU = "GhiChu";//todo
        string pattern = "dd/MM/yyyy";
        public static DataDBViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataDBViewModel();
                return _instance;
            }
        }
        private DataDBViewModel()
        {
            localContext = new DataClassesLocalDataContext();
            serverContext = new DataClassServerDataContext();
        }

        public IEnumerable getNote(string danhBa)
        {
            var data = (from x in serverContext.DocSos
                        where x.DanhBa == danhBa
                        orderby x.DocSoID descending
                        select new { x.Ky, x.Nam, danhBa, x.CodeMoi, x.TTDHNMoi, x.CSCu, x.CSMoi, x.TieuThuMoi, x.GhiChuDS, x.GhiChuKH, x.GhiChuTV }).ToList();
            return data;
        }

        public DataTable GetInfoCheckCustomer2(int count, string str, string danhBa)
        {


            var query = (from d in serverContext.DocSoLuuTrus
                         join k in serverContext.KhachHangs on d.DanhBa equals k.DanhBa
                         where d.DanhBa == danhBa
                         orderby d.DocSoID descending
                         select new { d.MLT1, d.CodeMoi, d.CSMoi, d.TieuThuMoi, d.DenNgay, k.TenKH, k.Hieu, k.Co, k.SoThan, k.HopDong, k.GB, k.DM, k.So, k.SoMoi, k.Duong, d.Ky, d.Nam })
                         .Take(12).ToList();
            DataTable table = new DataTable();
            table.Columns.Add("TenKH", typeof(string));
            table.Columns.Add("Hieu", typeof(string));
            table.Columns.Add("Co", typeof(string));
            table.Columns.Add("DanhBa", typeof(string));
            table.Columns.Add("MLT1", typeof(string));
            table.Columns.Add("Code", typeof(string));

            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row["TenKH"] = item.TenKH;
                row["Hieu"] = item.Hieu;
                row["Co"] = item.Co;
                row["DanhBa"] = danhBa;
                row["MLT1"] = item.MLT1;
                row["Code"] = item.CodeMoi;

                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetInfoCheckCustomer1(string str, string danhBa)
        {
            var query = (from d in serverContext.DocSos
                         join k in serverContext.KhachHangs on d.DanhBa equals k.DanhBa
                         where d.DanhBa == danhBa
                         orderby d.DocSoID descending
                         select new { d.GhiChuDS, d.MLT1, d.CodeMoi, d.CSMoi, d.TieuThuMoi, d.DenNgay, k.TenKH, k.Hieu, k.Co, k.SoThan, k.HopDong, k.GB, k.DM, k.So, k.SoMoi, k.Duong, d.Ky, d.Nam, k.SDT }).Take(12).ToList();
            DataTable table = new DataTable();
            table.Columns.Add(TTKH_COLUMN_TENKH, typeof(string));
            table.Columns.Add(TTKH_COLUMN_HIEU, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CO, typeof(string));
            table.Columns.Add(TTKH_COLUMN_SDT, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DUONG, typeof(string));
            table.Columns.Add(TTKH_COLUMN_SOTHAN, typeof(string));
            table.Columns.Add(TTKH_COLUMN_MLT1, typeof(string));
            table.Columns.Add(TTKH_COLUMN_HOPDONG, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(TTKH_COLUMN_GB, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DM, typeof(string));
            table.Columns.Add(TTKH_COLUMN_KY, typeof(string));
            table.Columns.Add(TTKH_COLUMN_NGAYDOC, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CODE, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CSMOI, typeof(string));
            table.Columns.Add(TTKH_COLUMN_TIEUTHU, typeof(string));
            table.Columns.Add(TTKH_COLUMN_GHICHU, typeof(string));
            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row[TTKH_COLUMN_TENKH] = item.TenKH;
                row[TTKH_COLUMN_HIEU] = item.Hieu;
                row[TTKH_COLUMN_CO] = item.Co;
                row[TTKH_COLUMN_SDT] = item.SDT;
                row[TTKH_COLUMN_DUONG] = item.SoMoi.Trim().Length == 0 ?
                    String.Format("{0} {1}", item.So, item.Duong) :
                    String.Format("{0} {1}", item.SoMoi, item.Duong);
                row[TTKH_COLUMN_SOTHAN] = item.SoThan;
                row[TTKH_COLUMN_MLT1] = item.MLT1;
                row[TTKH_COLUMN_HOPDONG] = item.HopDong;
                row[TTKH_COLUMN_DANHBA] = danhBa;
                row[TTKH_COLUMN_GB] = item.GB;
                row[TTKH_COLUMN_DM] = item.DM;
                row[TTKH_COLUMN_KY] = String.Format("{0}/{1}", item.Ky, item.Nam);
                row[TTKH_COLUMN_NGAYDOC] = item.DenNgay.Value.ToString(pattern);
                row[TTKH_COLUMN_CODE] = item.CodeMoi;
                row[TTKH_COLUMN_CSMOI] = item.CSMoi;
                row[TTKH_COLUMN_TIEUTHU] = item.TieuThuMoi;
                row[TTKH_COLUMN_GHICHU] = item.GhiChuDS;
                table.Rows.Add(row);
            }
            return table;

        }

        public bool Update(string code, string csm, string tieuThu, string ghiChuDS, string ghiChuMH, string ghiChuKH, string KHDS, DateTime ngayCapNhat, int nam, string ky, string dot, string danhBa)
        {
            var docSo = serverContext.DocSos.SingleOrDefault(row => row.DanhBa == danhBa && row.Nam == nam && row.Ky == ky && row.Dot == dot);
            if (docSo != null)
            {
                docSo.CodeMoi = code;
                docSo.CSMoi = Int16.Parse(csm);
                docSo.TieuThuMoi = Int16.Parse(tieuThu);
                docSo.GhiChuDS = ghiChuDS;
                docSo.GhiChuTV = ghiChuMH;
                docSo.GhiChuKH = ghiChuKH;
                docSo.TTDHNMoi = KHDS;
                docSo.StaCapNhat = "1";
                docSo.NgayCapNhat = ngayCapNhat;
                docSo.NVCapNhat = "";//todo         
                serverContext.SubmitChanges();

                return true;
            }
            return false;
        }

        public List<String> getDocsosByConditionCount(int year, String month, String date, int xGroup, String machine)
        {
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine
                           select x.DanhBa).ToList();
            return getData;
        }



        public bool getDocSosByDanhBa(String danhBa, int year, String month, String date, int xGroup, String machine)
        {

            bool result = false;

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.DanhBa == danhBa && x.May == machine
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        TODS = item.TODS,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        public void ThongKeDHNSauDocSo(string sqlStatement)
        {
            //var data = serverContext.ExecuteQuery<>(sqlStatement);
        }

        public void update(ItemCollection items)
        {
            foreach (var item in items)
            {
                DocSo docSo = item as DocSo;
                var v = (from x in serverContext.DocSos
                         where x.DanhBa == docSo.DanhBa && x.Nam == docSo.Nam && x.Ky == docSo.Ky && x.Dot == docSo.Dot
                         select x).FirstOrDefault();

                v.CodeMoi = docSo.CodeMoi;
                v.CSMoi = docSo.CSMoi;
                v.TieuThuMoi = docSo.TieuThuMoi;
                serverContext.SubmitChanges();


            }
        }
        public void update(DocSo docSo)
        {
            var v = (from x in serverContext.DocSos
                     where x.DanhBa == docSo.DanhBa && x.Nam == docSo.Nam && x.Ky == docSo.Ky && x.Dot == docSo.Dot
                     select x).FirstOrDefault();

            v.CodeMoi = docSo.CodeMoi;
            v.CSMoi = docSo.CSMoi;
            v.TieuThuMoi = docSo.TieuThuMoi;
            serverContext.SubmitChanges();

        }
        public List<ChuyenMayDS> getKH_ChuyenMayDS(string date, string machineLeft)
        {
            List<ChuyenMayDS> listData = new List<ChuyenMayDS>();
            var getData = (from x in localContext.DocSoLocals
                           where x.Dot == date && x.May == machineLeft
                           select new { x.DanhBa, x.MLT1, x.SoNhaCu, x.Duong }).ToList();
            foreach (var item in getData)
            {
                listData.Add(new ChuyenMayDS()
                {
                    DanhBa = item.DanhBa,
                    MLT = item.MLT1,
                    DiaChi = item.SoNhaCu + " " + item.Duong
                });
            }
            return listData;
        }



        public List<string> getDanhBasByCondition(int year, string month, string date, int xGroup, string machine)
        {
            var data = (from x in serverContext.DocSos
                        where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine && x.CSMoi > 0
                        select x.DanhBa).ToList();
            return data;
        }

        public DocSo getDocSoByDanhBa(string danhBa, int year, string month, string date, short xGroup, string machine)
        {
            var data = (from x in serverContext.DocSos
                        where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine && x.DanhBa == danhBa && x.CSMoi > 0
                        select x).FirstOrDefault();
            return data;
        }
        public List<DocSo> getAllDocSos(int year, string month, string date, int xGroup, string machine)
        {
            var data = (from x in serverContext.DocSos
                        where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup && x.May == machine
                        select x).ToList();
            return data;
        }

        public byte[] getImageByDanhBa(String danhBa, DateTime gioGhi)
        {

            using (DataClassServerDataContext tempServer = new DataClassServerDataContext())
            {
                var data = (from x in tempServer.HinhDHNs
                            where x.DanhBo == danhBa && x.CreateDate == gioGhi
                            orderby x.CreateDate
                            select x.Image).FirstOrDefault();
                if (data == null)
                    return null;

                return ((System.Data.Linq.Binary)data).ToArray();
            }
        }
        public ObservableCollection<DocSoLocal> getDistinctHoaDon(SoDaNhan selectedSoDaNhan)
        {
            ObservableCollection<DocSoLocal> listHoaDon = new ObservableCollection<DocSoLocal>();
            var hoaDons = (from x in localContext.DocSoLocals
                           where x.Nam == selectedSoDaNhan.nam && x.Ky == selectedSoDaNhan.ky && x.Dot == selectedSoDaNhan.dot && x.May == selectedSoDaNhan.may
                           select x).ToList();
            foreach (var item in hoaDons)
                listHoaDon.Add(item);
            return listHoaDon;
        }

        public XuLyCode8 GetXuLyCode8(string danhba)
        {
            var data = serverContext.ExecuteQuery<XuLyCode8>("declare @NgayDoc datetime select top 1 @NgayDoc = DenNgay from DocSo where DanhBa = '" + danhba + "' order by DocSoID desc Select top 1 *, DATEDIFF(DAY, NgayThay, @NgayDoc) as SoNgay from BaoThay where DanhBa ='" + danhba + "' and NgayThay is not null  order by BaoThayID desc").First();
            return data;
        }
        public int GetTieuThuMoi(string danhBa, string docSoID)
        {
            var data = serverContext.ExecuteQuery<int>("Select top 1 TieuThuMoi from DocSo where DanhBa='" + danhBa + "' and DocSoID <> " + docSoID + " order by DocSoID desc").First();
            return data;
        }
        public bool WriteSoDaNhan(int year, String month, String date, String machine, int count, int xGroup)
        {
            DataClassesLocalDataContext localDataContext = new DataClassesLocalDataContext();
            try
            {

                localDataContext.SoDaNhans.InsertOnSubmit(new SoDaNhan()
                {
                    So = year + "_" + month + "_" + date + "_" + machine,
                    SoLuong = count,
                    nam = year,
                    ky = month,
                    dot = date,
                    may = machine,
                    ToDS = xGroup
                });
                localDataContext.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }

        public bool getDocSosByCondition(int year, String month, String date, int xGroup)
        {

            bool result = false;

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
            var getData = (from x in serverContext.DocSos
                           where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        internal List<String> getTenKH(string danhBa)
        {
            var data = (from x in serverContext.KhachHangs
                        where x.DanhBa == danhBa
                        select new { x.TenKH, x.HopDong }).FirstOrDefault();
            return new List<string>() { data.TenKH, data.HopDong };
        }

        public List<String> getDistinctKHDS()
        {
            var data = (from x in serverContext.TTDHNs
                        orderby x.Stt
                        select x.TTDHN1).ToList();
            return data;
        }

        public MySoLenh getSoLenh(string danhBa)
        {
            var data = (from b in serverContext.ThongBaos
                        join t in serverContext.ThamSos on b.LoaiLenh.ToString() equals t.Code
                        where b.DanhBa == danhBa && t.CodeType == "TB"
                        orderby b.ID descending
                        select new
                        {
                            b.SoLenh,
                            t.CodeDesc,
                            b.ChiSo,
                            b.NgayKiem,
                            b.NoiDung,
                            b.Hieu,
                            b.Co,
                            b.NgayCapNhat
                        }).FirstOrDefault();
            if (data == null)
                return null;
            return new MySoLenh(danhBa, data.CodeDesc, data.ChiSo + "", data.NgayKiem.Value.ToString(pattern),
                data.NoiDung, data.Hieu, data.Co + "", data.NgayCapNhat.Value.ToString(pattern));
        }

        public MyBaoThay getBaoThay(string danhBa)
        {
            var data = (from b in serverContext.BaoThays
                        join t in serverContext.ThamSos on b.LoaiBT.ToString() equals t.Code
                        where b.DanhBa == danhBa && t.CodeType == "BT"
                        select new
                        {
                            danhBa,
                            t.CodeDesc,
                            b.CSGo,
                            b.CSGan,
                            b.SoThanMoi,
                            b.NgayThay,
                            b.NgayCapNhat
                        }).FirstOrDefault();
            if (data == null)
                return null;
            return new MyBaoThay(data.danhBa, data.CodeDesc, data.CSGo + "", data.CSGan + "",
                data.SoThanMoi, data.NgayThay.Value.ToString(pattern), data.NgayCapNhat.Value.ToString(pattern));
        }

        public ObservableCollection<int> getDistinctYear()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            var items = (from x in localContext.DocSoLocals
                         select x.Nam).Distinct().ToList();
            foreach (int item in items)
                lstYear.Add(item);
            return lstYear;
        }
        class Item
        {
            public int Year { get; set; }
            public int Month { get; set; }
        }
        public ObservableCollection<DocSo_1Ky> get12Months(int year, string Month, string danhBa)
        {



            int count = 0;
            int month = Int16.Parse(Month);
            List<Item> r = new List<Item>();
            String kyString;
            while (count <= 12)
            {
                month--;
                if (month == 0)
                {
                    year--;
                    month = 12;
                }
                kyString = month + "";
                if (month < 10)
                    kyString = "0" + month;
                r.Add(new Item()
                {
                    Year = year,
                    Month = month
                });
                //var data = (from x in serverContext.DocSos
                //            where x.DanhBa == danhBa && x.Nam == year && x.Ky == kyString
                //            select new { x.Ky, x.GIOGHI, x.CodeMoi, x.CSMoi, x.TieuThuMoi }).FirstOrDefault();
                //if (data != null)
                //    listDocSo.Add(new DocSo_1Ky(data.Ky + "/" + year, data.GIOGHI.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + ""));
                count++;
            }
            var whereStr = "and (";
            foreach (var s in r)
            {
                whereStr += String.Format(" (nam = {0} and ky = {1}) or", s.Year, s.Month);
            }
            whereStr = whereStr.Substring(0, whereStr.Length - 3);
            whereStr += ")";
            List<DocSo_1Ky> datas = serverContext.ExecuteQuery<DocSo>(String.Format("select docsoID, ky, gioghi, codemoi, csmoi, tieuthumoi from Docso where danhba = '" + danhBa + "' {0} " +
                "order by nam desc, ky desc", whereStr)).Select(data => new DocSo_1Ky(data.Ky + "/" + year, data.GIOGHI.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + "")).ToList();
            ObservableCollection<DocSo_1Ky> listDocSo = new ObservableCollection<DocSo_1Ky>(datas);
            return listDocSo;
        }

        public ObservableCollection<int> getDistinctYearServer()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            var items = (from x in serverContext.DocSos
                         select x.Nam).Distinct().OrderByDescending(x => x).ToList();
            foreach (int item in items)
                lstYear.Add(item);
            return lstYear;
        }
        public ObservableCollection<String> getDistinctMonth()
        {
            ObservableCollection<String> lstMonth = new ObservableCollection<String>();
            var items = (from x in localContext.DocSoLocals
                         select x.Ky).Distinct().ToList();
            foreach (String item in items)
                lstMonth.Add(item);
            return lstMonth;
        }
        public ObservableCollection<String> getDistinctMonthServer(int year)
        {
            ObservableCollection<String> lstMonth = new ObservableCollection<String>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year
                         select x.Ky).Distinct().ToList();
            foreach (String item in items)
                lstMonth.Add(item);
            return lstMonth;
        }
        public ObservableCollection<String> getDistinctDate()
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            var items = (from x in localContext.DocSoLocals
                         select x.Dot).Distinct().ToList();
            foreach (String item in items)
                lstDate.Add(item);
            return lstDate;
        }
        public ObservableCollection<String> getDistinctDateServer(int year, String month)
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month
                         select x.Dot).Distinct().ToList();
            foreach (String item in items)
                lstDate.Add(item);
            return lstDate;
        }
        public ObservableCollection<int> getDistinctGroup()
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();

            var items = (from x in localContext.DocSoLocals
                         select x.TODS).Distinct().ToList();
            foreach (int item in items)
                lstGroup.Add(item);

            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachine()
        {
            ObservableCollection<String> lstGroup = new ObservableCollection<String>();

            var items = (from x in localContext.DocSoLocals
                         select x.May).Distinct().ToList();
            foreach (String item in items)
                lstGroup.Add(item);

            return lstGroup;
        }
        public ObservableCollection<int> getDistinctGroupServer(int year, String month, String date)
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();
            var items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date
                         select x.TODS).Distinct().ToList();
            foreach (int item in items)
                lstGroup.Add(item);
            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachineServer(int year, String month, String date, int xGroup)
        {
            ObservableCollection<String> lstMachine = new ObservableCollection<String>();
            List<String> items;
            if (xGroup == 0)
                items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date
                         select x.May).Distinct().ToList();
            else
                items = (from x in serverContext.DocSos
                         where x.Nam == year && x.Ky == month && x.Dot == date && x.TODS == xGroup
                         select x.May).Distinct().ToList();
            foreach (String item in items)
                lstMachine.Add(item);
            return lstMachine;
        }
        public ObservableCollection<SoDaNhan> getDistinctSoDaNhan(int year, String month, String date, int xGroup)
        {
            ObservableCollection<SoDaNhan> listSoDaNhan = new ObservableCollection<SoDaNhan>();
            var soDaNhans = (from x in localContext.SoDaNhans
                             where x.nam == year && x.ky == month && x.dot == date && x.ToDS == xGroup
                             select x).ToList();
            foreach (var item in soDaNhans)
                listSoDaNhan.Add(item);
            return listSoDaNhan;
        }

    }
}

