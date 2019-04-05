Migration
PM> add-migration -c eGoatDDDDbContext Initial -project eGoatDDD.Persistence
PM> update-database




Drop Databases
Use eGoatDDD;
	DROP TABLE __EFMigrationsHistory,
	AspNetRoleClaims,
	AspNetUserClaims,
	AspNetUserLogins,
	AspNetUserRoles,
	AspNetUsers,
	AspNetUserTokens,
	Package,
	Categories,
	Product,
	AppUser,
	Loan,
	[Loan Details],
	AspNetRoles


	        /* public async Task ManageRoles(SelectOptionList roles)
        {
            var roleList = await GetUserRoles();
            //when add new role value and label will be the same
            var roleToAdd = roles.SelectOptionViewModels.Where(x => x.Label == x.Value).ToList();
            var roleToRemove = roleList.Where(existingRole => roles.SelectOptionViewModels
                .All(item => item.Value != existingRole.Id && existingRole.Name != "Administrator")).ToList();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (roleToRemove.Any())
                    {
                        var roleName = roleToRemove.Select(x => x.Name).ToArray();
                        foreach (var roleRemove in roleToRemove)
                        {
                            var userInRole = await _userService.GetListRoleOfUser(roleRemove.Name);
                            if (!userInRole.Any()) continue;
                            foreach (var user in userInRole)
                            {
                                await _userService.RemoveFromRolesAsync(user, roleName);
                            }

                            var role = await _unitOfWork.Repository<IdentityRole>().FindAsync(x =>
                                x.Name.Equals(roleRemove.Name, StringComparison.OrdinalIgnoreCase));
                            await _unitOfWork.Repository<IdentityRole>().DeleteAsync(role);
                        }
                    }

                    if (roleToAdd.Any())
                    {
                        var roleName = roleToAdd.Select(x => x.Label).ToArray();
                        await _userService.AddUserRoles(roleName);
                    }

                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
            }
        } */

      
        /* public async Task<bool> AccountToggle(AccountToggleViewModel accountToggleVm)
        {
            var account = await _unitOfWork.Repository<User>().Query().Where(acc => acc.Id == accountToggleVm.AccountId).FirstOrDefaultAsync();
            if (account == null)
            {
                return false;
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    account.EmailConfirmed = accountToggleVm.ToogleFlag;
                    await _unitOfWork.Repository<User>().UpdateAsync(account);
                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
            }

            return true;
        } */

        /* public async Task<bool> AddNewUser(UserInputViewModel userInputVm)
        {
            var user = new User { UserName = userInputVm.Username, Email = userInputVm.Email };
            var randomPassword = RandomString.GenerateRandomString(AppEnum.MinPasswordChar);
            var result = await _userService.CreateAsync(user, randomPassword);

            if (!result.Succeeded)
            {
                return false;
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _userService.AddUserToRolesAsync(user, userInputVm.Roles);

                    var context = _httpContextAccessor.HttpContext;
                    var code = await _userService.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = _urlHelperExtension.EmailConfirmationLink(user.Id, code, context.Request.Scheme);
                    var emailOptions = new EmailOptions
                    {
                        Url = callbackUrl,
                        Password = randomPassword,
                        UserName = userInputVm.Username
                    };

                    await _emailSender.SendEmailAsync(userInputVm.Email, "", emailOptions, EmailType.AccountConfirm);
                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
            }

            return true;
        }

        public async Task<bool> ValidateDuplicateAccountInfo(UserAccountValidateObject accountValidateObject)
        {
            switch (accountValidateObject.Key)
            {
                case "UserName":
                    var isUserNameDuplicate = await _unitOfWork.Repository<User>().Query().Where(acc =>
                            acc.UserName.Equals(accountValidateObject.Value, StringComparison.OrdinalIgnoreCase))
                        .AnyAsync();
                    return isUserNameDuplicate;
                case "Email":
                    var isEmailDuplicate = await _unitOfWork.Repository<User>().Query().Where(acc =>
                            acc.Email.Equals(accountValidateObject.Value, StringComparison.OrdinalIgnoreCase))
                        .AnyAsync();
                    return isEmailDuplicate;
                default:
                    return false;
            }
        } */