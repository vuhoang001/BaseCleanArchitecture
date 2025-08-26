using Application.Dtos;

namespace Application.Handlers.OrgLevel.Commands.Create;

public class CreateOrgLevelCommand(OrgLevelCreate request) : ICommand<bool>;