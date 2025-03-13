// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("1icpe67qJRZWCNFeuKWPeHdBtGATH0OLy20gJTx+RadwO8f4nvDIb4DaptcM0DlkCbVY91fh6yO/bPH0u0slUXHkoDd2DtiQpuzN8UyvZrYA7xUU7h/NG3D6PYknQhn477KHO9O6pMO4esjGEpUXR1y4T3ArC49VtDc5Nga0Nzw0tDc3NpJvBpOEUsAPwjR0azsriUkWZTsB2ZvKrAvswQa0NxQGOzA/HLB+sME7Nzc3MzY1ylhR0qKNHVE40CC4oRfSPtOS/YfHI1HfIutuC4vP+aPKVETBJuuVLbtLgyec5E97LjKXaXY41FguggeAqqO38HbMv1R1lVcJJsfjai69oz4oNLfss3ojWhpfS6qm0bRYTzBXwk703xpURBu6aTQ1NzY3");
        private static int[] order = new int[] { 6,3,5,4,13,11,13,12,13,12,12,12,12,13,14 };
        private static int key = 54;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
