using MyNewHiringWebApp.Domain.Entities;

public sealed class CandidateSkill
{
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    public int Level { get; set; } //from 1 to 5 

    public CandidateSkill() { } 

   
    public CandidateSkill(Candidate candidate, Skill skill, int level = 1)
    {
        Candidate = candidate ?? throw new ArgumentNullException(nameof(candidate));
        Skill = skill ?? throw new ArgumentNullException(nameof(skill));
        SetLevel(level);
    }

    public void SetLevel(int level)
    {
        if (level < 1 || level > 5) throw new ArgumentOutOfRangeException(nameof(level));
        Level = level;
    }
}
