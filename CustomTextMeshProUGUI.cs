using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CustomTextMeshProUGUI : TextMeshProUGUI
{
    bool UpdateData = false;

    public void UGUIUpdate(in float distanceMove, in float animationSpeed, in float maxRotation)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        // メッシュ更新
        if (UpdateData)
            ForceMeshUpdate();
        
        UpdateData = m_textInfo.characterCount != 0;
        for (int index = 0; index < m_textInfo.characterCount; index++)
        {
            var charaInfo = m_textInfo.characterInfo[index];

            if (!charaInfo.isVisible)
                continue;
            //Materialのindex
            int materialIndex = charaInfo.materialReferenceIndex;
            //頂点のindex
            int vertexIndex = charaInfo.vertexIndex;
            float sinValue = Mathf.Sin(Time.time * animationSpeed + 10 * index);

            //移動
            Vector3[] destVertices = m_textInfo.meshInfo[materialIndex].vertices;
            Vector3 move = distanceMove * (Vector3.down * sinValue);
            destVertices[vertexIndex + 0] += move;
            destVertices[vertexIndex + 1] += move;
            destVertices[vertexIndex + 2] += move;
            destVertices[vertexIndex + 3] += move;
            //色
            Color32[] destcolors32 = m_textInfo.meshInfo[materialIndex].colors32;
            byte alpha = (byte)(Mathf.Abs(sinValue) * 255.0f);
            destcolors32[vertexIndex + 0].a = alpha;
            destcolors32[vertexIndex + 1].a = alpha;
            destcolors32[vertexIndex + 2].a = alpha;
            destcolors32[vertexIndex + 3].a = alpha;
            //回転
            Vector3 offset = (destVertices[vertexIndex + 1] + destVertices[vertexIndex + 3]) / 2;
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, maxRotation * sinValue), Vector3.one);
            destVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 0] - offset) + offset;
            destVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 1] - offset) + offset;
            destVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 2] - offset) + offset;
            destVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 3] - offset) + offset;

            
        }

        if(UpdateData)
        UpdateVertexData();
    }
}