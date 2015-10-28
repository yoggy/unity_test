test18_billboard
====
![amin.gif](amin.gif)

code
----
    void UpdateBillboard()
    {
        if (targetCamera == null) return;

        Vector3 p0 = gameObject.transform.position;
        Vector3 p1 = targetCamera.transform.position;

        // y²‰ñ“]‚Ì‚İBx-z•½–Ê‚Ì‚İ‚ÅŒü‚«‚ğl‚¦‚éB
        p0.y = 0;
        p1.y = 0;

        var rot = Quaternion.FromToRotation(Vector3.back, p1 - p0);

        gameObject.transform.rotation = rot;
    }
