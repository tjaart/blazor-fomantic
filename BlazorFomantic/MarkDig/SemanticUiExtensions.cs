namespace Markdig.SemanticUi
{
    public static class SemanticUiExtensions
    {
        public static MarkdownPipelineBuilder UseSemanticUi(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.Add(new SemanticUiExtension());
            return pipeline;
        }
    }
}