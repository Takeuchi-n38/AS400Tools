﻿using System;

namespace Delta.Pdfs
{
    public class PdfObject
    {
        // Format object as string value to write into PDF file
        private static readonly string FORMAT_WRITABLE_DATA = "{0} 0 obj \n{1}\nendobj \n";

        // Object ID
        public int objId;

        // Object data
        private string data;

        //*
        //* Create a PdfObject by ID and data
        //* @param objId - object ID
        //* @param data - string value

        public PdfObject(int objId, string data)
        {
            this.objId = objId;
            this.data = data;
        }

        //*
        //* Create a PdfObject by ID and null data
        //* @param objId - object ID

        public PdfObject(int objId)
        {
            this.objId = objId;
            data = null;
        }

        //*
        //* Create a PdfObject by unknown ID and data
        //* @param data - string value

        public PdfObject(string data)
        {
            objId = -1;
            this.data = data;
        }

        //*
        //* Create a PdfObject by unknown ID and null data

        public PdfObject()
        {
            objId = -1;
        }

        public void SetObjId(int objId)
        {
            this.objId = objId;
        }

        public int GetObjId()
        {
            return objId;
        }

        public void SetData(string data)
        {
            this.data = data;
        }

        public string GetData()
        {
            return data;
        }

        //*
        //* Create data to write into PDF file.
        //* @return

        public string ToWritableData()
        {
            return string.Format(FORMAT_WRITABLE_DATA, objId, data);
        }
    }
}
