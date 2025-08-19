using Application.Dtos;

namespace Application.OrgLevel.Commands.Create;

public class CreateOrgLevelCommand(OrgLevelCreate request) : ICommand<bool>;