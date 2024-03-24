using CarBill.bussinesData.Models;

namespace CarBill.vm
{
    public class BillRowWithImagesForm
    {
        public int billRowId { get; set; }
        public int amount { get; set; }
        public decimal total_priceForBillRow { get; set; }
        public int billId { get; set; }
        public int sparePartId { get; set; }
        public List<FormFile> PhotosInFormFiles { get; set; }



    }
    public class AdditionBillData
    {
        public List<BillRowWithImagesForm> billRows { get; set; }
        public string comment { get; set; }
        public string currentReading { get; set; }
        public string maintancePeriod { get; set; }
        public decimal totalAmount { get; set; }
        public int discountRate { get; set; }
        public FormFile videoInFormFile { get; set; }
        
    }
    public class BillFormData
    {
        public Car carData { get; set; }
        public AdditionBillData additionBillData { get; set; }
        public List<MaintananceType> cats { get; set; }
    }
}
