using Microsoft.VisualBasic.FileIO;
using ServiciiAuto.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServiciiAutoProject.Helpers
{
    public class CsvReaderHelper
    {
        public List<Record> GetRecordsFromCsv(string csvFilePath)
        {
            return ConvertDataTableToRecordsList(GetRecordsDataFromCsvFile(csvFilePath));
        }

        private List<Record> ConvertDataTableToRecordsList(DataTable recordsTable)
        {
            var records = new List<Record>();

            foreach (DataRow row in recordsTable.Rows)
            {
                if (row["Description"] == null || 
                    (row["Description"] != null && string.IsNullOrEmpty(row["Description"].ToString()) 
                    || row["Description"].ToString().ToUpper() == "DE STERS"))
                {
                    continue;
                }

                var notes = row["Notes"].ToString();
                var clientInfo = GetClientNameFromNotes(notes);
                var registrationNumber = GetRegistrationNumberFromDescription(row["Description"].ToString());
                var type = GetTypeFromDescription(row["Description"].ToString());

                records.Add(new Record()
                {
                    Id = Guid.NewGuid(),
                    ExpirationDate = DateTime.Parse(row["Date"].ToString()),
                    AdditionalInfo = string.Format("{0} | {1}", row["Description"].ToString(),row["Notes"].ToString()),
                    ClientName = clientInfo.Name,
                    PhoneNumber = clientInfo.PhoneNumber,
                    CarRegistartionNumber = registrationNumber,
                    RecordTypeName = type,
                    CreationDate = DateTime.Now
                });
            }

            return records;
        }

        private Client GetClientNameFromNotes(string notes)
        {
            var numberPart = notes.ToArray().Where(x => char.IsDigit(x)).Select(x => x);
            var alphaPart = notes.ToArray().Where(x => !char.IsDigit(x)).Select(x => x);

            var phoneNumberFromNotes = String.Concat(numberPart);
            var clientNameFromNotes = String.Concat(alphaPart);
            if (phoneNumberFromNotes.Length > 10)
            {

            }
            var phoneNumber = phoneNumberFromNotes.Length == 10 ? phoneNumberFromNotes : phoneNumberFromNotes.Length > 10 ? String.Concat(phoneNumberFromNotes.Take(10)) : string.Format("0{0}", phoneNumberFromNotes);
            var clientName = clientNameFromNotes.Contains('(') || clientNameFromNotes.Contains(')')  ? string.Empty: clientNameFromNotes;

            phoneNumber = phoneNumber.Length < 9 ? string.Empty : phoneNumber;

            return new Client() { Name = clientName, PhoneNumber = phoneNumber };
        }

        private string GetRegistrationNumberFromDescription(string description)
        {
            var stringsInDescription = description.Split(' ');
            var registratioNumber = string.Empty;

            foreach (var part in stringsInDescription)
            {
                registratioNumber = part.Length >= 6 ? part : string.Empty;
            }

            return registratioNumber;
        }

        private string GetTypeFromDescription(string description)
        {
            var stringsInDescription = description.Split(' ');
            var type = string.Empty;

            if (stringsInDescription.Count() > 2)
            {
                if (!string.IsNullOrEmpty(stringsInDescription[1]) && stringsInDescription[1].Length < 7)
                {
                    type = stringsInDescription[1];
                }
            }

            return type;
        }

        private DataTable GetRecordsDataFromCsvFile(string filePath)
        {
            var csvData = new DataTable();

            using (var csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();

                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }

                    csvData.Rows.Add(fieldData);
                }
            }

            return csvData;
        }
    }
}