using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintScript.Model
{
    public class Queries
    {
        public static string testQuery = "SELECT\n" +
                "\tIFNULL(T4.\"Address\", T0.\"CardName\") AS \"Odbiorca\",\n" +
                "\tIFNULL(T4.\"Street\", '') || ' ' || IFNULL(T4.\"StreetNo\", '') \"Street\",\n" +
                "\tIFNULL(T4.\"ZipCode\", '') || ' ' || IFNULL(T4.\"City\", '') \"City\",\n" +
                "\tIFNULL(T0.\"Address2\", T0.\"Address\") AS \"Adres odbiorcy\",\n" +
                "\tRIGHT('000000' || CAST(T0.\"DocNum\" AS NVARCHAR(15)), 6) AS \"Advice note\",\n" +
                "\tT9.\"U_DrawNoFinal\" AS \"Part no\",\n" +
                "\tT2.\"Quantity\" AS \"Quantity\",\n" +
                "\tT3.\"U_NrEGTwgKlienta\" AS \"Supplier\",\n" +
                "\t'TEST' AS \"Serial\",\n" +
                "\tT10.\"Descr\" AS \"Dock/Gate\",\n" +
                "\tT3.\"GlblLocNum\" AS \"GTL\",\n" +
                "\tT9.\"FrgnName\" AS \"Description\",\n" +
                "\tT9.\"U_DrawNoRaw\" AS \"Supplier part no\",\n" +
                "\tTO_DATE(T0.\"DocDate\") AS \"Date\",\n" +
                "\tT15.\"U_LabelNo\" AS \"Serien\",\n" +
                "\tT15.\"U_QtyOnLabel\",\n" +
                "\tT2.\"ItemCode\",\n" +
                "\tT2.\"LineNum\"\n" +
                "FROM\n" +
                "\tOADM T1, ODLN T0\n" +
                "\tLEFT OUTER JOIN DLN1 T2 ON T2.\"DocEntry\" = T0.\"DocEntry\"\n" +
                "\tINNER JOIN OCRD T3 ON T3.\"CardCode\" = T0.\"CardCode\"\n" +
                "\tINNER JOIN CRD1 T4 ON T4.\"CardCode\" = T3.\"CardCode\" AND T4.\"AdresType\" = 'S'\n" +
                "\tINNER JOIN OITM T9 ON T9.\"ItemCode\" = T2.\"ItemCode\" AND T9.\"QryGroup3\" = 'Y'\n" +
                "\tLEFT OUTER JOIN UFD1 T10 ON T10.\"TableID\" = 'OITM' AND T10.\"FieldID\" = 10 AND T10.\"FldValue\" = T9.\"U_DockGate\"\n" +
                "\tLEFT OUTER JOIN \"@CT_NEM\" T15 ON T15.\"U_DocEntry\" = T2.\"DocEntry\" AND T15.\"U_ItemCode\" = T2.\"ItemCode\" AND T15.\"U_LineNum\" = T2.\"LineNum\"\n" +
                "WHERE\n" +
                "\tT0.\"DocEntry\" = 1499\n" +
                "ORDER BY T15.\"U_LabelNo\"";
    }
}
