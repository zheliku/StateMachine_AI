﻿namespace DrawXXL
{
    using UnityEngine;

    public class Ray_fadeableAnimSpeed : ParentOf_Lines_fadeableAnimSpeed_3D
    {
        public Vector3 start = Vector3.zero;
        public Vector3 direction = Vector3.one;
        public Color endColor = default(Color); //Can be ignored if the line should have only one color. If it is specified then the line will have a fading color transition, starting with "color"(link) at the start of the line and ending with "endColor" at the end of the line
        public DrawBasics.LineStyle style = DrawBasics.LineStyle.solid;
        public float stylePatternScaleFactor = 1.0f;
        public float alphaFadeOutLength_0to1 = 0.0f;
        public bool skipPatternEnlargementForLongLines = false;
        public bool skipPatternEnlargementForShortLines = false;

        public Ray_fadeableAnimSpeed(Vector3 start, Vector3 direction)
        {
            this.start = start;
            this.direction = direction;
        }

        public void Draw()
        {
            if (DXXLWrapperForUntiysBuildInDrawLines.CheckIfDrawingIsCurrentlySkipped()) { return; }
            if (UtilitiesDXXL_Colors.IsDefaultColor(endColor))
            {
                lineAnimationProgress = InternalDraw(start, direction, color, width, text, style, stylePatternScaleFactor, animationSpeed, lineAnimationProgress, customAmplitudeAndTextDir, flattenThickRoundLineIntoAmplitudePlane, endPlates_size, alphaFadeOutLength_0to1, enlargeSmallTextToThisMinTextSize, durationInSec, hiddenByNearerObjects, skipPatternEnlargementForLongLines, skipPatternEnlargementForShortLines);
            }
            else
            {
                lineAnimationProgress = InternalDrawColorFade(start, direction, color, endColor, width, text, style, stylePatternScaleFactor, animationSpeed, lineAnimationProgress, customAmplitudeAndTextDir, flattenThickRoundLineIntoAmplitudePlane, endPlates_size, alphaFadeOutLength_0to1, enlargeSmallTextToThisMinTextSize, durationInSec, hiddenByNearerObjects, skipPatternEnlargementForLongLines, skipPatternEnlargementForShortLines);
            }
        }

        public static LineAnimationProgress InternalDraw(Vector3 start, Vector3 direction, Color color, float width, string text, DrawBasics.LineStyle style, float stylePatternScaleFactor, float animationSpeed, LineAnimationProgress precedingLineAnimationProgress, Vector3 customAmplitudeAndTextDir, bool flattenThickRoundLineIntoAmplitudePlane, float endPlates_size, float alphaFadeOutLength_0to1, float enlargeSmallTextToThisMinTextSize, float durationInSec, bool hiddenByNearerObjects, bool skipPatternEnlargementForLongLines, bool skipPatternEnlargementForShortLines)
        {
            if (DXXLWrapperForUntiysBuildInDrawLines.CheckIfDrawingIsCurrentlySkipped()) { return null; }
            if (UtilitiesDXXL_Log.ErrorLogForInvalidVectors(start, "start")) { return null; }
            if (UtilitiesDXXL_Log.ErrorLogForInvalidVectors(direction, "direction")) { return null; }

            return Line_fadeableAnimSpeed.InternalDraw(start, start + direction, color, width, text, style, stylePatternScaleFactor, animationSpeed, precedingLineAnimationProgress, customAmplitudeAndTextDir, flattenThickRoundLineIntoAmplitudePlane, endPlates_size, alphaFadeOutLength_0to1, enlargeSmallTextToThisMinTextSize, durationInSec, hiddenByNearerObjects, skipPatternEnlargementForLongLines, skipPatternEnlargementForShortLines);
        }

        public static LineAnimationProgress InternalDrawColorFade(Vector3 start, Vector3 direction, Color startColor, Color endColor, float width, string text, DrawBasics.LineStyle style, float stylePatternScaleFactor, float animationSpeed, LineAnimationProgress precedingLineAnimationProgress, Vector3 customAmplitudeAndTextDir, bool flattenThickRoundLineIntoAmplitudePlane, float endPlates_size, float alphaFadeOutLength_0to1, float enlargeSmallTextToThisMinTextSize, float durationInSec, bool hiddenByNearerObjects, bool skipPatternEnlargementForLongLines, bool skipPatternEnlargementForShortLines)
        {
            if (DXXLWrapperForUntiysBuildInDrawLines.CheckIfDrawingIsCurrentlySkipped()) { return null; }
            if (UtilitiesDXXL_Log.ErrorLogForInvalidVectors(start, "start")) { return null; }
            if (UtilitiesDXXL_Log.ErrorLogForInvalidVectors(direction, "direction")) { return null; }
            return UtilitiesDXXL_DrawBasics.LineColorFade(start, start + direction, startColor, endColor, width, text, style, stylePatternScaleFactor, animationSpeed, precedingLineAnimationProgress, customAmplitudeAndTextDir, flattenThickRoundLineIntoAmplitudePlane, alphaFadeOutLength_0to1, enlargeSmallTextToThisMinTextSize, durationInSec, hiddenByNearerObjects, skipPatternEnlargementForLongLines, skipPatternEnlargementForShortLines, null, false, endPlates_size, 1.0f);
        }
    }

}
