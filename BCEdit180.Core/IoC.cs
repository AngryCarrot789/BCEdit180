namespace BCEdit180.Core {
    public static class IoC {
        public static IUIManager UI => ServiceManager.GetService<IUIManager>();
    }
}