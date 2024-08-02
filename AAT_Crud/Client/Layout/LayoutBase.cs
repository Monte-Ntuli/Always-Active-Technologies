using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Client.Layout
{
    public class LayoutBase : ComponentBase
    {
        [Inject]
        public ILocalStorageService localStorage { get; set; }
    }
}
