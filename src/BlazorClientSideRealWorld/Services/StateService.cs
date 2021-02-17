using System;
using BlazorClientSideRealWorld.Models;

namespace BlazorClientSideRealWorld.Services
{
    public class StateService
    {
        private readonly IApiService api;

        public StateService(IApiService api)
        {
            this.api = api;
            User = new UserModel();
        }

        public event Action OnUserChange;

        private void NotifyUserChanged() => OnUserChange?.Invoke();

        public ErrorsModel Errors { get; private set; }
        public UserModel User { get; private set; }

        public bool IsSignedIn => User?.Token != null;

        public void UpdateUser(UserModel user)
        {
            var oldToken = User?.Token;
            var newToken = user?.Token;

            if (oldToken != newToken)
            {
                User = user;

                if (newToken != null)
                {
                    api.SetToken(newToken);
                }
                else
                {
                    api.ClearToken();
                }

                NotifyUserChanged();
            }
        }
    }
}
