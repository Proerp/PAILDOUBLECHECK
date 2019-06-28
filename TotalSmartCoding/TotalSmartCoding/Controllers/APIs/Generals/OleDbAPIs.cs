using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Generic;

using AutoMapper;

using TotalBase;
using TotalBase.Enums;

using TotalCore.Repositories.Generals;

using TotalModel.Models;

using TotalDTO.Generals;
using TotalDTO.Inventories;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class OleDbAPIs
    {
        private readonly IOleDbAPIRepository oleDbAPIRepository;

        public OleDbAPIs(IOleDbAPIRepository oleDbAPIRepository, GlobalEnums.MappingTaskID mappingTaskID)
        {
            this.oleDbAPIRepository = oleDbAPIRepository;
            this.oleDbAPIRepository.MappingTaskID = mappingTaskID;
        }

        public BindingList<ColumnMappingDTO> GetColumnMappings()
        {
            return Mapper.Map<IList<ColumnMapping>, BindingList<ColumnMappingDTO>>(this.oleDbAPIRepository.GetColumnMappings().ToList());
        }

        public void SaveColumnMapping(int columnMappingID, string columnMappingName)
        {
            this.oleDbAPIRepository.SaveColumnMapping(columnMappingID, columnMappingName);
        }

        public List<string> GetExcelSheets(string excelFile)
        {
            return this.oleDbAPIRepository.GetExcelSheets(excelFile);
        }

        public DataTable OpenExcelSheet(string excelFile, string sheetName)
        {
            return this.oleDbAPIRepository.OpenExcelSheet(excelFile, sheetName);
        }

        public DataTable OpenExcelSheet(string excelFile, string sheetName, string querySelect)
        {
            return this.oleDbAPIRepository.OpenExcelSheet(excelFile, sheetName, querySelect);
        }

    }
}