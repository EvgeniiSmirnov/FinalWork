namespace FinalWork.Models.UI;

public class Project
{
    public string ProjectName { get; }
    public string ProjectCode { get; }
    public string Description { get; }
    public bool IsPublicProjectAccessType { get; }

    private Project(string projectName, string projectCode, string description, bool IsPublicType)
    {
        ProjectName = projectName;
        ProjectCode = projectCode;
        Description = description;
        IsPublicProjectAccessType = IsPublicType;
    }

    public class Builder
    {
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public bool IsPublicProjectAccessType { get; set; }

        public Builder SetProjectName(string projectName)
        {
            ProjectName = projectName;
            return this;
        }

        public Builder SetProjectCode(string projectCode)
        {
            ProjectCode = projectCode;
            return this;
        }

        public Builder SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public Builder SetCheckboxPublicProjectAccessType(bool IsPublicType)
        {
            IsPublicProjectAccessType = IsPublicType;
            return this;
        }

        public Project Build()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
                throw new InvalidOperationException("ProjectName can not be null");

            return new Project(
                ProjectName,
                ProjectCode,
                Description,
                IsPublicProjectAccessType);
        }
    }
}