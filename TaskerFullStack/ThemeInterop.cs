using Microsoft.JSInterop;

namespace TaskerFullStack
{ // Must match your assembly name
    public static class ThemeInterop
    {
        [JSInvokable("OnThemeChanged")]
        public static Task OnThemeChanged()
        {
            Console.WriteLine("Theme change invoked from JS.");
            return Task.CompletedTask;
        }
    }
}
