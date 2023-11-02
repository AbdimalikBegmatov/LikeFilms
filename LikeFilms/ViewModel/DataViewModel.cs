namespace LikeFilms.ViewModel
{
    public class DataViewModel<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
