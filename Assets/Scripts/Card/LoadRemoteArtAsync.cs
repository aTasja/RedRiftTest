using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Card
{
    public class LoadRemoteArtAsync : MonoBehaviour
    {
        private const string WebImage = "https://picsum.photos/185/275";
        
        [SerializeField] private Material _materialPrefab;
        [SerializeField] private Image _image;

        private Texture2D _texture;
        private async void Start ()
        {
            _texture = await GetRemoteTexture(WebImage);
            var material = new Material(_materialPrefab);
            material.mainTexture = _texture;
            _image.material = material;
        }

        private static async Task<Texture2D> GetRemoteTexture ( string url )
        {
            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            var _asyncOp = www.SendWebRequest();

            while( _asyncOp.isDone==false )
                await Task.Delay( 1000/30 );//30 hertz

            if( www.result != UnityWebRequest.Result.Success )
            {
                Debug.Log($"{www.error}, URL:{www.url}");
                return null;
            }
            else
            {
                return DownloadHandlerTexture.GetContent(www);
            }
        }

        private void OnDestroy () => Dispose();
        private void Dispose () => Object.Destroy(_texture);
    }
}
