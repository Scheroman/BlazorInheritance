using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorInheritance.Client.Components
{
    public class MyBaseComponent : ComponentBase
    {

        public static Object Content = new ();
        protected int Counter { get; set; }
        protected RenderTreeBuilder Builder { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
            Builder = builder;
			base.BuildRenderTree(builder);
		}


		protected virtual void DoSomething()
        {
           Counter++;
		} 

        protected virtual void OnNextStep()
        {

        }

        protected virtual bool IsValid()
        {
            return true;
        }

		
	}
}
