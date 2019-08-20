namespace DotnetAlgos.Bsts
{
    public class BstNode
    {
        public int value;
        public BstNode left;
        public BstNode right;
        public BstNode(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        }
    }
}