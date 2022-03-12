namespace ECommerceSite.Models
{
    //used to limit how many crates are shown to user at once
    public class CrateCatalogueViewModel
    {
        //use constructor to force use of NumPages
        public CrateCatalogueViewModel(List<Crate> crates, int lastPage, int currPage)
        {
            Crates = crates;
            NumPages = lastPage;
            CurrentPage = currPage;
        }
        public List<Crate> Crates { get; private set; }
        /// <summary>
        /// The number of pages to be created
        /// that have crates to populate
        /// </summary>
        public int NumPages { get; private set; }

        public int CurrentPage { get; private set; }
    }
}
