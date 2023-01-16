using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Windows;
using UnityEngine.UI;

public class MathCalculator : MonoBehaviour
{
    //Notes:
    // ====EXCEPTIONS====
    // 1. Denumerator != 0
    // 2. Both Numerators must be written
    // 3. Both Denumerators must be written
    // 4. Non-negative values must be written
    // 5. Integer values must be written
    // 6. If after calculation Denumerator is 0 throw "Fraction is undefined, Result Denumerator is 0"
    // 7. Calculation Type must be selected
    // 8. Multiple calculation Types selected
    // ====MATHEMATICAL LOGIC====
    // 1. Addition with identical Denumerators - Add Numerators together
    // 2. Addition with different Denumerators - Multiply Denumerators together to get common Denumerator
    //   2.1 Multiply Numerators with the opposite Denumerator
    // 3. Subtraction with identical Denumerators - Subtract Numerators from each other
    // 4. Subtraction with different Denumerators - Multiply Denumerators together to get common Denumerator
    //   4.1 Multiply Numerators with the opposite Denumerator
    // 5. Multiplication - Multiply Numerator with Numerator and Denumerator with Denumerator
    // 6. Division - Flip Second Fraction and Multiply Numerator with Numerator and Denumerator with Denumerator
    // ==================

    [Header("Left-Side Values")]
    public int leftNumerator;
    public int leftDenumerator;
    [Header("Right-Side Values")]
    public int rightNumerator;
    public int rightDenumerator;
    [Header("Result Values")]
    public int resultNumerator;
    public int resultDenumerator;
    [Header("TMP Input Fields")]
    public TMP_InputField leftNumeratorInputField;
    public TMP_InputField leftDenumeratorInputField;
    public TMP_InputField rightNumeratorInputField;
    public TMP_InputField rightDenumeratorInputField;
    [Header("Result Text Fields")]
    public TMP_Text resultNumeratorTextField;
    public TMP_Text resultDenumeratorTextField;
    [Header("Calculation Types")]
    public Toggle additionType;
    public Toggle subtractionType;
    public Toggle multiplicationType;
    public Toggle divisionType;

    string input;

    void Start()
    {
        // These fields are here for safety - To be sure that these fields are taking the correct Component
    }
    void Update()
    {
        // Placeholder in case needed in the future
        if (!additionType.isOn && !subtractionType.isOn && !multiplicationType.isOn && !divisionType.isOn)
        {
            SetAllCalcTypesInteractable();
        }
    }

    public void Quit()
    {
        // In case of Calculator Program being used in a Build and the user wants to leave
        Application.Quit();
    }

    public void Addition()
    {
        // Converting InputFields to int values
        int leftNum = Convert.ToInt32(leftNumeratorInputField.text);
        int leftDenum = Convert.ToInt32(leftDenumeratorInputField.text);
        int rightNum = Convert.ToInt32(rightNumeratorInputField.text);
        int rightDenum = Convert.ToInt32(rightDenumeratorInputField.text);

        if (leftDenum == rightDenum)
        {
            int resultNum = leftNum + rightNum;
            int resultDenum = leftDenum;
            resultNumeratorTextField.text = resultNum.ToString();
            resultDenumeratorTextField.text = resultDenum.ToString();
            // Since both Denumerators are the same, there is no difference if left or right Denumerators are used for this field
            // Exception is not necessary as there is no room for fails
        }
        else
        {
            int resultNum = (leftNum * rightDenum) + (rightNum * leftDenum);
            int resultDenum = leftDenum * rightDenum;
            resultNumeratorTextField.text = resultNum.ToString();
            resultDenumeratorTextField.text = resultDenum.ToString();
        }
        throw new Exception("No values are present or you went out of the loop (Safe Exception) - [Addition()]");
    }
    public void Subtraction()
    {
        // Converting InputFields to int values
        int leftNum = Convert.ToInt32(leftNumeratorInputField.text);
        int leftDenum = Convert.ToInt32(leftDenumeratorInputField.text);
        int rightNum = Convert.ToInt32(rightNumeratorInputField.text);
        int rightDenum = Convert.ToInt32(rightDenumeratorInputField.text);

        if (leftDenum == rightDenum)
        {
            if (leftNum >= rightNum)
            {
                // This Logic should prevent from getting an Exception: "Values cannot be negative"
                int resultNum = leftNum - rightNum;
                int resultDenum = leftDenum;
                resultNumeratorTextField.text = resultNum.ToString();
                resultDenumeratorTextField.text = resultDenum.ToString();
                if (resultNum <= 0)
                {
                    throw new Exception("Result value is 0 or less, positive values are required - [Subtraction()]");
                }
            } else if (leftNum <= rightNum)
            {
                // This Logic should prevent from getting an Exception: "Values cannot be negative"
                int resultNum = rightNum - leftNum;
                int resultDenum = leftDenum;
                resultNumeratorTextField.text = resultNum.ToString();
                resultDenumeratorTextField.text = resultDenum.ToString();
                if (resultNum <= 0)
                {
                    throw new Exception("Result value is 0 or less, positive values are required - [Subtraction()]");
                }
            }
            throw new Exception("Values cannot be negative or you went out of the loop (Safe Exception) - [Subtraction()]");
        }
        else
        {
            int resultNum = (leftNum * rightDenum) - (rightNum * leftDenum);
            int resultDenum = leftDenum * rightDenum;
            resultNumeratorTextField.text = resultNum.ToString();
            resultDenumeratorTextField.text = resultDenum.ToString();
            if (resultNum <= 0)
            {
                throw new Exception("Result value is 0 or less, positive values are required - [Subtraction()]");
            }
        }
        throw new Exception("No values present or you left the loop (Safe Exception) - [Subtraction()]");
    }
    public void Multiplication()
    {
        // Converting InputFields to int values
        int leftNum = Convert.ToInt32(leftNumeratorInputField.text);
        int leftDenum = Convert.ToInt32(leftDenumeratorInputField.text);
        int rightNum = Convert.ToInt32(rightNumeratorInputField.text);
        int rightDenum = Convert.ToInt32(rightDenumeratorInputField.text);

        int resultNum = leftNum * rightNum;
        int resultDenum = leftDenum * rightDenum;
        resultNumeratorTextField.text = resultNum.ToString();
        resultDenumeratorTextField.text = resultDenum.ToString();
        if (resultNum <= 0 || resultDenum <= 0)
        {
            throw new Exception("Result value is 0 or less, positive values are required - [Multiplication()]");
        }
    }
    public void Division()
    {
        // Converting InputFields to int values
        int leftNum = Convert.ToInt32(leftNumeratorInputField.text);
        int leftDenum = Convert.ToInt32(leftDenumeratorInputField.text);
        int rightNum = Convert.ToInt32(rightNumeratorInputField.text);
        int rightDenum = Convert.ToInt32(rightDenumeratorInputField.text);

        int resultNum = leftNum * rightDenum;
        int resultDenum = leftDenum * rightNum;
        resultNumeratorTextField.text = resultNum.ToString();
        resultDenumeratorTextField.text = resultDenum.ToString();
        if (resultNum <= 0 || resultDenum <= 0)
        {
            throw new Exception("Result value is 0 or less, positive values are required - [Division()]");
        }
    }

    public void CalculatePressed()
    {
        if (additionType.isOn)
        {
            Addition();
        }
        else if (subtractionType.isOn)
        {
            Subtraction();
        }
        else if (multiplicationType.isOn)
        {
            Multiplication();
        }
        else if (divisionType.isOn)
        {
            Division();
        }
        else
        {
            throw new Exception("Multiple Calculation Types selected or no Types selected, please choose one - [CalculatePressed()]");
        }
        Debug.Log("Calculate Pressed");

    }


    public void ClearPressed()
    {
        // Setting fields to standard 0 , showing that values are empty
        leftNumeratorInputField.text = " ";
        leftDenumeratorInputField.text = "";
        rightNumeratorInputField.text = " ";
        rightDenumeratorInputField.text = " ";
        resultNumeratorTextField.text = " ";
        resultDenumeratorTextField.text = " ";
    }
    public void SetCalcTypeAdd()
    {
        SetSubtractionUninteractable();
        SetMultiplicationUninteractable();
        SetDivisionUninteractable();
        Debug.Log("Addition selected");
    }
    public void SetCalcTypeSub()
    {
        SetAdditionUninteractable();
        SetMultiplicationUninteractable();
        SetDivisionUninteractable();
        Debug.Log("Subtraction selected");
    }
    public void SetCalcTypeMulti()
    {
        SetAdditionUninteractable();
        SetSubtractionUninteractable();
        SetDivisionUninteractable();
        Debug.Log("Multiplication selected");
    }
    public void SetCalcTypeDivis()
    {
        SetAdditionUninteractable();
        SetSubtractionUninteractable();
        SetMultiplicationUninteractable();
        Debug.Log("Division selected");
    }
    public void SetAdditionUninteractable()
    {
        additionType.isOn = false;
        additionType.interactable = false;
    }
    public void SetSubtractionUninteractable()
    {
        subtractionType.isOn = false;
        subtractionType.interactable = false;
    }
    public void SetMultiplicationUninteractable()
    {
        multiplicationType.isOn = false;
        multiplicationType.interactable = false;
    }
    public void SetDivisionUninteractable()
    {
        divisionType.isOn = false;
        divisionType.interactable = false;
    }
    public void SetAllCalcTypesInteractable()
    {
        additionType.interactable = true;
        subtractionType.interactable = true;
        multiplicationType.interactable = true;
        divisionType.interactable = true;
    }

    private bool FractionsInputtedCheck(bool fractionsFilled)
    {
        if (leftNumerator >= 0 && leftDenumerator > 0 && rightNumerator >= 0 && rightDenumerator > 0)
        {
          return fractionsFilled = true;
        }
        else
        {
            throw new ArgumentException("Value fields are not present, please all input values - [FractionsInputtedCheck()]");
        }
        
    }

}
