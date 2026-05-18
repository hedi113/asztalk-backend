using Solution.Services.Models;

namespace Solution.Services;

public interface ICharacterService
{
    Task<CharacterModel> CreateAsync(CharacterUpdateModel updateModel);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<CharacterModel>> GetAllAsync();
    Task<CharacterModel> GetByIdAsync(int id);
    Task<CharacterModel> UpdateAsync(CharacterUpdateModel updateModel, int id);
}
