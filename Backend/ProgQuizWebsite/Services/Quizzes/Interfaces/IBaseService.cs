namespace ProgQuizWebsite.Services.Quizzes.Interfaces
{
    public interface IBaseService<T>
    {
        public Task UpdateCache();
    }
}
