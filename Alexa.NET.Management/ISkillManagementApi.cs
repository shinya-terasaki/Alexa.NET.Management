using System.Threading.Tasks;
using Alexa.NET.Management.Skills;
using Refit;

namespace Alexa.NET.Management
{
    [Headers("Authorization: Bearer")]
    public interface ISkillManagementApi
    {
        [Get("skills/{skillId}")]
        Task<Skill> Get(string skillId);

        [Post("skills/{vendorId}")]
        Task<SkillId> Create(string vendorId, [Body]Skill skill);

        [Put("skills/{skillId}")]
        Task<SkillId> Update(string skillId, [Body] Skill skill);

        [Get("skills/{skillId}/status")]
        Task<SkillStatus> Status(string skillId);
    }
}
