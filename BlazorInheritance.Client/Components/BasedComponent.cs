using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorInheritance.Client.Components
{
	public class BasedComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
		protected RenderTreeBuilder Builder { get; set; }
        private RenderFragment _renderFragment;
        private RenderHandle _renderHandle;

        public BasedComponent()
        {
            
            _renderFragment = builder =>
            {
                //_hasPendingQueuedRender = false;
                //_hasNeverRendered = false;
                BuildRenderTree(builder);
            };

            //Type type = _renderFragment.Target.GetType();
            //IComponent comp = Builder.GetFrames().Array.FirstOrDefault().C;//-> gets child component;
            //ComponentBase baseLEL = comp as ComponentBase;
            //if (baseLEL != null)
            //{
            //    baseLEL.
            //}
        }

        public void Attach(RenderHandle renderHandle)
        {
            // This implicitly means a ComponentBase can only be associated with a single
            // renderer. That's the only use case we have right now. If there was ever a need,
            // a component could hold a collection of render handles.
            if (_renderHandle.IsInitialized)
            {
                throw new InvalidOperationException($"The render handle is already set. Cannot initialize a {nameof(ComponentBase)} more than once.");
            }

            _renderHandle = renderHandle;
        }

        public Task HandleEventAsync(EventCallbackWorkItem item, object? arg)
        {
            
            return Task.CompletedTask;
        }

        public Task OnAfterRenderAsync()
        {
            BasedComponent parent = _renderFragment.Target as BasedComponent;
            parent?.AfterChildRender();
            return Task.CompletedTask;
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            _renderHandle.Render(_renderFragment);
            return Task.CompletedTask;
        }

        protected virtual void BuildRenderTree(RenderTreeBuilder builder)
        {
            // Developers can either override this method in derived classes, or can use Razor
            // syntax to define a derived class and have the compiler generate the method.

            // Other code within this class should *not* invoke BuildRenderTree directly,
            // but instead should invoke the _renderFragment field.
        }

        protected virtual void AfterChildRender()
        {

        }

        Task IHandleAfterRender.OnAfterRenderAsync()
        {
            //var firstRender = !_hasCalledOnAfterRender;
            //_hasCalledOnAfterRender = true;

            //OnAfterRender(firstRender);

            return OnAfterRenderAsync();

            // Note that we don't call StateHasChanged to trigger a render after
            // handling this, because that would be an infinite loop. The only
            // reason we have OnAfterRenderAsync is so that the developer doesn't
            // have to use "async void" and do their own exception handling in
            // the case where they want to start an async task.
        }

    }
}
