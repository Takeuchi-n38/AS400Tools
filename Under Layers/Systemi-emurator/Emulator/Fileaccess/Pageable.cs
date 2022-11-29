namespace Systemi_emurator.Fileaccess
{
    public class Pageable
    {
        public Pageable(int page, int size)
        {
            this.page = page;
            this.size = size;
        }
        public int page { get; set; }
        public int size { get; set; }
    }
}
